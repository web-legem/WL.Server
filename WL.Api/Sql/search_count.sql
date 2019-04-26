create or replace function search_count(
  words_to_search in varchar2 default null,
  entity in number default null,
  document_type in number default null,
  doc_number in varchar2 default null,
  pub_year in number default null
)
return number
is
  res number;
  query varchar2(32767);

  anyParameterIsNotNull boolean := search_count.words_to_search is not null
    or search_count.entity is not null
    or search_count.document_type is not null
    or search_count.doc_number is not null
    or search_count.pub_year is not null;

  whereClauseEntity VARCHAR(32767) := CASE WHEN search_count.entity IS NOT NULL
      THEN
         CASE WHEN search_count.words_to_search IS NOT NULL
         THEN ' AND '
         ELSE ' ' END
         || search_count.entity || ' = d."EntityId"'
      ELSE '' END;

  whereClauseDocumentType VARCHAR(32767) := CASE WHEN search_count.document_type IS NOT NULL
      THEN
         CASE WHEN search_count.words_to_search IS NOT NULL
          OR search_count.entity Is NOT NULL
         THEN ' AND '
         ELSE ' ' END
         || search_count.document_type || ' = d."DocumentTypeId"'
      ELSE '' END;

  whereClauseNumber VARCHAR(32767) := CASE WHEN search_count.doc_number IS NOT NULL
      THEN
         CASE WHEN search_count.words_to_search IS NOT NULL
          OR search_count.entity Is NOT NULL
          OR search_count.document_type is not null
         THEN ' AND '
         ELSE ' ' END
         || ' REGEXP_LIKE(d."Number", ''.*' || search_count.doc_number || '.*'')'
      ELSE '' END;

  whereClauseYear VARCHAR(32767) := CASE WHEN search_count.pub_year IS NOT NULL
      THEN
         CASE WHEN search_count.words_to_search IS NOT NULL
          OR search_count.entity Is NOT NULL
          OR search_count.document_type is not null
          or search_count.doc_number is not null
         THEN ' AND '
         ELSE ' ' END
         || search_count.pub_year || ' = d."PublicationYear"'
      ELSE '' END;

  whereClauseWordsToSearch varchar2(32767) := CASE WHEN search_count.words_to_search IS NOT NULL
    THEN ' CONTAINS(f."CONTENTS", :1, 1) > 0'
    ELSE '' END;
begin
  query := '
select
  count(*)
from "Files" f
JOIN "Documents" d ON f."DocumentId" = d."Id"
JOIN "Entities" e ON d."EntityId" = e."Id"
JOIN "DocumentTypes" dt ON d."DocumentTypeId" = dt."Id"
' || CASE WHEN anyParameterIsNotNull
  THEN ' WHERE '
  ELSE '' END
  || whereClauseWordsToSearch
  || whereClauseEntity
  || whereClauseDocumentType
  || whereClauseNumber
  || whereClauseYear;

  IF search_count.words_to_search is not null
  then
    execute immediate query into res using search_count.words_to_search;
  else
    execute immediate query into res;
  end if;

  return res;
end search_count;