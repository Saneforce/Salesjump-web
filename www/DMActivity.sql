select Convert(varchar,DtTm,103) Date,Convert(varchar,DtTm,108) Time,StateName,Zone_name,Area_name,Sf_Name,vdm.Territory_name,msl.Stockist_Name DB,msl.Territory,DrName+'/'+tsf.RouteNm as Venue,isnull(title,'') Mode,
FIRST_VALUE(Dateandtime) over(partition by SF,DrCode,Stkid order by DtTm) FC,
LAST_VALUE(Dateandtime) over(partition by SF,DrCode,Stkid order by DtTm
ROWS BETWEEN UNBOUNDED PRECEDING AND UNBOUNDED FOLLOWING)  LC,Product_Name SKU,Quantity Sales,free,'http://tiesar.sanfmcg.com/photos/'+SF+'_'+imgurl ImgUrl
 --select *
 FROM  vw_TSR_Direct_Markecting vdm with (nolock)
inner join  tbINHSessFeedbk tsf with (nolock) on tsf.SF = vdm.Sf_Code 
Left Outer Join  tbINHSessFeedbk_Order_Details tsod  with (nolock) on tsod.RwID = tsf.RwID 
Left Outer Join Activity_Event_Captures i         On  Identification='Promotion' and Ekey = i.DCRDetNo
Inner Join Mas_Stockist msl with (nolock) on  tsf.Stkid = msl.Stockist_Code  



Date - State -Zone – AREA-USER - HQ - DB – Town- Outlet / Venue-FC Time-LC Time- Brand--SKU – SALES – FREE-Image Thumbnail
select case when title = 'FC' then cast(Dateandtime as time) else ''  end as FC,
       case when title = 'LC' then cast(Dateandtime as time) else '' end as LC,
	   case when title = 'FC' then imgurl else ''  end as FCimgurl,
	   case when title = 'LC' then imgurl else ''  end as LCimgurl 
	   
	   
from Activity_Event_Captures
where  Identification='Promotion'

group by imgurl ,title,Dateandtime


select *from Activity_Event_Captures 
where  Identification='Promotion'
 
select LAG
