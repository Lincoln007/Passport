
--
-- VACCOUNT
--
CREATE OR REPLACE FORCE VIEW "VACCOUNT" 
  AS 
  select
  A.ID,
  A.USERNAME,
  A.TRUENAME,
  A.CREATE_TIME,
  A.STATUS,
  A.DELETED,
  D.NAME AS DEPARTMENT,
  R.NAME AS RANK
    from ACCOUNT A
    LEFT JOIN DEPARTMENT D
    ON
    A.DEPARTMENT_ID = D.ID
    LEFT JOIN RANK R
    ON
    A.RANK_ID = R.ID
;