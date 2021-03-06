create or replace function "GETSYSTIME"
  return date
is
  res date;
begin
  select fixedSysTime into res from ApplicationInfo;
  return coalesce(res, SYSDATE);
end;
