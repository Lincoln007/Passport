-- Create table
create table USER_ACCOUNT_GROUP
(
  id         NUMBER(10) not null,
  account_id NUMBER(10) not null,
  group_id   NUMBER(10) not null,
  deleted    NUMBER(1) not null
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
alter table USER_ACCOUNT_GROUP
  add constraint PK_ACCOUNT_GROUPS_ID primary key (ID)
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
