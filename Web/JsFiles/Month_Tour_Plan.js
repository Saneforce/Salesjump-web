function submitValidate()
{
	var dcrCode;
	dcrCode="";
	document.frmCall_Plan.Mode.value = "Insert"

	if(document.frmCall_Plan.selChosen.length<=1)
	{
		alert("Select MSL No")
		return false;
		
	}
	for(var i=0;i< document.frmCall_Plan.selChosen.length-1;i++)
	{
	
		document.frmCall_Plan.hidDcrCode_List.value=""
	
		if (dcrCode=="")
		{
			dcrCode=document.frmCall_Plan.selChosen[i].value
		}
		else
		{
			dcrCode=dcrCode+","+document.frmCall_Plan.selChosen[i].value
		}
		document.frmCall_Plan.hidDcrCode_List.value=dcrCode
	}
	
	
	return true
}	


