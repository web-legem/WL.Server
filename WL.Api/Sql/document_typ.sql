create or replace type document_typ as object
(
  "DocumentId" number,
  "DocumentTypeId" number,
  "EntityId" number,
  "Number" varchar2(10),
  "PublicationYear" number,
  "PublicationDate" timestamp,
  "FileDocumentId" number
);