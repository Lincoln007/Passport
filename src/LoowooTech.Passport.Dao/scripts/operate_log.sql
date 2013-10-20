-- Create table
create table OPERATE_LOG
(
  id          NUMBER(38) not null,
  account_id  NUMBER(38) not null,
  action      VARCHAR2(256 CHAR) not null,
  create_time DATE not null
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
-- Create/Recreate primary, unique and foreign key constraints 
alter table OPERATE_LOG
  add constraint PK_LOG_ID primary key (ID)
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
