-- Create table
create table USER_ACCOUNT
(
  id              NUMBER(10) not null,
  username        VARCHAR2(128 CHAR) not null,
  password        VARCHAR2(32 CHAR) not null,
  create_time     DATE default sysdate not null,
  last_login_time DATE not null,
  last_login_ip   VARCHAR2(20 CHAR) not null,
  deleted         NUMBER(1) default 0 not null,
  role            NUMBER(5) not null,
  truename        VARCHAR2(32 CHAR)
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
alter table USER_ACCOUNT
  add constraint PK_ID primary key (ID)
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
