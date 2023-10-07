

select * from Mas_Salesforce where Sf_Code='admin'
select * from map_Designation where designation_Actual_Name_sl_No=1

select * from mas_division

select designation_Sl_No from map_Designation m 
inner join Mas_salesforce S 
on m.designation_Actual_Name_sl_No=s.designation_Actual_Name_sl_No 
where sf_code='admin'

sp_helptext "QryDivEdit"
sp_helptext "MasDivSP"

 select State_Code,StateName,ShortName from Mas_State
 
 select case when count(Division_Code)>0 then max(Division_Code) else 0 end 
 from Mas_Division
 select * from Mas_State
order by 1 
 insert into Mas_State values(5,'Andhra Pradesh','AP')