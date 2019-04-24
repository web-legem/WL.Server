create or replace function search_dt(
  page in number default null,
  page_size in number default null,
  words_to_search in varchar2 default null,
  entity in number default null,
  document_type in number default null,
  doc_number in varchar2 default null,
  pub_year in number default null,
  order_by IN VARCHAR2 DEFAULT 'DEFAULT',
  descend in boolean default false
)
return document_tbl pipelined
is
  res sys_refcursor;
  query varchar2(32767);
  tuple document_typ := document_typ(
    NULL,
    NULL,
    NULL,
    NULL,
    NULL,
    NULL,
    NULL
    );

  anyParameterIsNotNull boolean := search_dt.words_to_search is not null
    or search_dt.entity is not null
    or search_dt.document_type is not null
    or search_dt.doc_number is not null
    or search_dt.pub_year is not null;

  whereClauseEntity VARCHAR(32767) := CASE WHEN search_dt.entity IS NOT NULL
      THEN
         CASE WHEN search_dt.words_to_search IS NOT NULL
         THEN ' AND '
         ELSE ' ' END
         || search_dt.entity || ' = d."EntityId"'
      ELSE '' END;

  whereClauseDocumentType VARCHAR(32767) := CASE WHEN search_dt.document_type IS NOT NULL
      THEN
         CASE WHEN search_dt.words_to_search IS NOT NULL
          OR search_dt.entity Is NOT NULL
         THEN ' AND '
         ELSE ' ' END
         || search_dt.document_type || ' = d."DocumentTypeId"'
      ELSE '' END;

  whereClauseNumber VARCHAR(32767) := CASE WHEN search_dt.doc_number IS NOT NULL
      THEN
         CASE WHEN search_dt.words_to_search IS NOT NULL
          OR search_dt.entity Is NOT NULL
          OR search_dt.document_type is not null
         THEN ' AND '
         ELSE ' ' END
         || ' REGEXP_LIKE(d."Number", ''.*' || search_dt.doc_number || '.*'')'
      ELSE '' END;

  whereClauseYear VARCHAR(32767) := CASE WHEN search_dt.pub_year IS NOT NULL
      THEN
         CASE WHEN search_dt.words_to_search IS NOT NULL
          OR search_dt.entity Is NOT NULL
          OR search_dt.document_type is not null
          or search_dt.doc_number is not null
         THEN ' AND '
         ELSE ' ' END
         || search_dt.pub_year || ' = d."PublicationYear"'
      ELSE '' END;

  whereClauseWordsToSearch varchar2(32767) := CASE WHEN search_dt.words_to_search IS NOT NULL
    THEN ' CONTAINS(f."CONTENTS", :1, 1) > 0'
    ELSE '' END;

  orderByClause VARCHAR(32767) := CASE search_dt.order_by
      WHEN 'ENTIDAD' THEN ' ORDER BY e."Name"'
      WHEN 'TIPO_DOCUMENTO' THEN ' ORDER BY dt."Name"'
      WHEN 'NUMERO' THEN ' ORDER BY d."Number"'
      WHEN 'ANIO_PUBLICACION' THEN ' ORDER BY d."PublicationYear"'
      ELSE
         CASE WHEN search_dt.words_to_search IS NOT NULL
         THEN ' ORDER BY score(1)'
         ELSE '' END
      END;

  descClause VARCHAR2(32767) := CASE WHEN (orderByClause LIKE '%ORDER BY%')
      AND (descend = TRUE OR search_dt.order_by = 'DEFAULT')
      THEN ' DESC'
      ELSE '' END;

  offsetClause VARCHAR(32767) := ' OFFSET ' || (
    CASE WHEN page IS NULL THEN 1 ELSE page END
    - 1) *
    CASE WHEN page_size IS NULL THEN 20 ELSE page_size END
    || ' ROWS';

  fetchClause VARCHAR(32767) := ' FETCH NEXT ' ||
    CASE WHEN page_size IS NULL THEN 20 ELSE page_size END
    || ' ROWS ONLY';
begin
  query := '
select
  d."DocumentId",
  d."DocumentTypeId",
  d."EntityId",
  d."Number",
  d."PublicationYear",
  d."PublicationDate",
  d."FileDocumentId"
from "Files" f
JOIN "Documents" d ON f."DocumentId" = d."DocumentId"
JOIN "Entities" e ON d."EntityId" = e."EntityId"
JOIN "DocumentTypes" dt ON d."DocumentTypeId" = dt."DocumentTypeId"
' || CASE WHEN anyParameterIsNotNull
  THEN ' WHERE '
  ELSE '' END
  || whereClauseWordsToSearch
  || whereClauseEntity
  || whereClauseDocumentType
  || whereClauseNumber
  || whereClauseYear
  || orderByClause
  || descClause
  || offsetClause
  || fetchClause;

  IF search_dt.words_to_search is not null
  then
    open res for query using search_dt.words_to_search;
  else
    open res for query;
  end if;

  loop fetch res into
        tuple."DocumentId" ,
        tuple."DocumentTypeId" ,
        tuple."EntityId" ,
        tuple."Number" ,
        tuple."PublicationYear" ,
        tuple."PublicationDate" ,
        tuple."FileDocumentId";
    exit when res%NOTFOUND;
    PIPE ROW(tuple);
  END LOOP;
  CLOSE res;

  return;
end search_dt;