CREATE TABLE ApplicationInfo (
  id NUMBER NOT NULL ENABLE, 
  version NUMBER NOT NULL ENABLE, 
  fixedSysTime DATE, 
  CONSTRAINT "APPLICATIONINFO_PK" PRIMARY KEY (id) ENABLE, 
  CONSTRAINT "APPLICATIONINFO_SINGLETON" CHECK (id=1) ENABLE
);

INSERT INTO ApplicationInfo VALUES(1,1,NULL);

CREATE OR REPLACE PROCEDURE usp_SetDbVersion
	(in_version IN number)
AS
BEGIN
	UPDATE ApplicationInfo SET version=in_version where id=1;
END;



