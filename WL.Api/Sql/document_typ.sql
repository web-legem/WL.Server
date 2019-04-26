create or replace type document_typ as object
(
  "Id" number,
  "DocumentTypeId" number,
  "EntityId" number,
  "Number" varchar2(10),
  "PublicationYear" number,
  "PublicationDate" timestamp,
  "FileDocumentId" number
);