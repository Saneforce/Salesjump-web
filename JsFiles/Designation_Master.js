<script language="JavaScript">
     var fl=0       
    function Srl($x){with($x.style){if(display=='none')display='';else display='none';}}
    function Validate() 
    {
        var a=0
        with(aspnetForm)
        {
            var DesigName = ctl00$ContentPlaceHolder1$DD_DesignationName.value;
	        if (DesigName== "-1" || DesigName.charAt()=="" )
	        {
               fl=1
	           alert("select the Designation name");	    
    	       ctl00$ContentPlaceHolder1$DD_DesignationName.focus();		
	           return false   
	        }
	        DName = ctl00$ContentPlaceHolder1$txtActual_DesignationName.value;
	        if (DName== "" || DName.charAt(0)=="" )
	        {
    	        fl=1	    
	    	    ctl00$ContentPlaceHolder1$txtActual_DesignationName.focus();		
		        return false   
	        }
    	    SN1 = ctl00$ContentPlaceHolder1$txtShortName1.value;
	        if (SN1== "" || SN1.charAt(0)=="" )
	        {
	            fl=1
    		    ctl00$ContentPlaceHolder1$txtShortName1.focus();
		        return false   
	        }
		    Mname = ctl00$ContentPlaceHolder1$DD_MenuName.value;	  
		    if (Mname== "0" || Mname.charAt()=="" )
	        { 
	            fl=1 
	            alert("Select the MenuName");
		        ctl00$ContentPlaceHolder1$DD_MenuName.focus();
		        return false   
	        }	           	        
	        if (ctl00$ContentPlaceHolder1$HiddesigCode.value=="")
	        {
	            with(ctl00_ContentPlaceHolder1_chkDivision)
	            {
	                $f=0;
	                for($i=0;$i<rows.length;$i++)
	                {
	                    $c=rows[$i].cells;
	                    for($k=0;$k<$c.length;$k++)
	                    {
	                        $o=$c[$k].childNodes;
	                        for($j=0;$j<$o.length;$j++)
	                            if($o[$j].tagName=='INPUT')
	                                if($o[$j].checked==true)$f=1;
	                    }
	                }
	            }
    	        if($f==0)
	            {
	                alert('Select the Division');
	                return false;
	            }
	            confirmMesg = confirm("Are You Sure to Save this Record ?")
                if (confirmMesg == true)
			    {	
				    return true;
			    }
			    else
			    {
				    return false;
			    }
             }	    
         }// End of with
         document.aspnetForm.ctl00_ContentPlaceHolder1_DD_DesignationName.focus();
    }// end of function
    
     function getfocus()
    {
        var _cTRL=event.srcElement;             
        if (fl==0)
        {
            _cTRL.style.backgroundColor='LavenderBlush';
        }
        else
        {            
            _cTRL.style.backgroundColor='Lavender';
        }
        fl=0;
    }
</script>

