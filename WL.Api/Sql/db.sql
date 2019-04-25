CREATE USER webl IDENTIFIED BY webl;

CREATE TABLESPACE tb_webl
    DATAFILE 'C:/oracle/oradata/tests/dat_webl.dbf'
    SIZE 10M AUTOEXTEND ON;

GRANT RESOURCE, CONNECT, CTXAPP, DBA TO webl;

GRANT EXECUTE ON CTXSYS.CTX_DDL TO webl;
GRANT EXECUTE ON CTXSYS.CTX_CLS TO webl;
GRANT EXECUTE ON CTXSYS.CTX_DOC TO webl;
GRANT EXECUTE ON CTXSYS.CTX_OUTPUT TO webl;
GRANT EXECUTE ON CTXSYS.CTX_QUERY TO webl;
GRANT EXECUTE ON CTXSYS.CTX_REPORT TO webl;
GRANT EXECUTE ON CTXSYS.CTX_THES TO webl;
GRANT EXECUTE ON CTXSYS.CTX_ULEXER TO webl;

GRANT CREATE ANY DIRECTORY TO webl;

ALTER USER webl DEFAULT TABLESPACE tb_webl;