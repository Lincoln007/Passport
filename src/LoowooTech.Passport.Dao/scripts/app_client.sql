-- Create table
create table APP_CLIENT
(
  id            NUMBER(10) not null,
  client_id     VARCHAR2(32 CHAR) not null,
  client_secret VARCHAR2(32 CHAR) not null,
  create_time   DATE not null,
  hosts         VARCHAR2(256 CHAR) not null,
  deleted       NUMBER(1) not null
)
tablespace USERS
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
-- Create/Recreate indexes 
create index IX_CLIENT_CLIENTID on APP_CLIENT (CLIENT_ID)
  tablespace USERS
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
-- Create/Recreate primary, unique and foreign key constraints 
alter table APP_CLIENT
  add constraint PK_CLIENT_ID primary key (ID)
  using index 
  tablespace USERS
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
