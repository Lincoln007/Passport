-- Create table
create table USER_ACCOUNT
(
  id              NUMBER not null,
  username        VARCHAR2(128) not null,
  password        VARCHAR2(32) not null,
  create_time     DATE default sysdate not null,
  last_login_time DATE not null,
  last_loing_ip   VARCHAR2(20) not null,
  deleted         NUMBER(1) default 0 not null
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
