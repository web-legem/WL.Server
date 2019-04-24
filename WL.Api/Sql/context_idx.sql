CREATE INDEX "CONTENTS_IDX"
    ON "Files"( "CONTENTS" )
    INDEXTYPE is CTXSYS.CONTEXT
    PARAMETERS('replace metadata sync (on commit)');