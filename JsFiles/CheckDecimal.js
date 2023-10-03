<script>
function decimalCheck(x)
{
	
	count	= 0
	aa		= event.keyCode;
	w		= x.value
	len		= w.length
		if(aa==46)
		{
			count=count+1
		}

		for(i=0;i<len;i++)
		{
			y=w.charAt(i)

			if(y==".")
			{
				count=count+1
			}
			if(count>1)	
			{
			return false
			}
		}

	if(((aa < 48 || aa >57)&& (aa!=46) && (aa!=13))) 
	{ 
		alert('Enter Only Numbers');
//		x.value=""
		return false
	} 
		
		
} 


function firstChar(val)
	{
	   var fc,len,ind
		len=val.length
		fc=""
		fc=val.substring(0,1)
		ind=val.indexOf(" ")
		while (ind!=-1)	
		{
			if (val.charAt(ind+1)!=' ') fc=fc+val.charAt(ind+1)
			val=val.substring(ind+1,len)
			len=val.length
			ind=val.indexOf(" ")
		}	
		
		return(fc);
	}

function caps(field)
{
	var arr,s,i;
	s=field.value;
	arr = s.split(" ");
	field.value = "";
	if(s!="")
	{
		for(i=0;i<arr.length;i++)
		field.value = field.value + arr[i].substring(0,1).toUpperCase() + arr[i].substring(1,arr[i].length)+" ";
	}
}

function correctDiscount(y)
{
	if(parseFloat(y.value)>100.00)
	{
		alert("Discount cannot be greater than 100")
		y.focus()
	}
}

function correctDecimal(x)
{

	var sub,sublen,test
	test = x.value
	test = test.substring(0,test.indexOf("."))
	len=x.value.length
	sublen=test.length;
	if(parseInt(len)>0 && parseInt(sublen)==0)
	{
		sublen=len	
	}
	if(x.value==".")
	{
		alert("Only decimal point is not allowed")
		x.value=""
		x.focus();
		return false
	}
	//else if(parseInt(sublen)>7)
	//{
	//	alert("More than seven digit is not allowed")
	//	x.focus();
	//	return false
	//} 
	chkDecimal(x.value,x)
	
}


function correctSplCaseDecimal(x)
{
	var sub,sublen,test
	test = x.value
	test = test.substring(0,test.indexOf("."))
	len=x.value.length
	sublen=test.length;
	if(parseInt(len)>0 && parseInt(sublen)==0)
	{
		sublen=len	
	}
	if(x.value==".")
	{
		alert("Only decimal point is not allowed")
		x.value=""
		return false
	}
	else if(parseInt(sublen)>15)
	{
		alert("More than seven digit is not allowed")
		x.focus();
		return false
	} 
	
}
function chkDecimal(a,z)
{

	var a,x,y,i
	regInt=/^[0-9]|\.$/
	x=a.length
//	alert(a)
	count=0
	for(i=0;i<x;i++)
	{
		if(!regInt.test(a.charAt(i)))
		{ 
			alert("Enter Only Numbers");
			//z.value="";
			z.focus()
			return false
		}
		if(a.charAt(i)=='.')
		{
			count++			
			if(count==1)
				y=i
		}
	}
	if(count>1)
	{
		alert("Only one Decimal Point is allowed")
		z.focus()
		return false;
	}
	if(x-y-1>2)
	{
		alert("Only two decimal digits are allowed")
		z.focus()
		return false;
	}
	return true
}





function correctDecimal1(x)
{
	var sub,sublen,test
	test = x.value
	test = test.substring(0,test.indexOf("."))
	len=x.value.length
	sublen=test.length;
	if(parseInt(len)>0 && parseInt(sublen)==0)
	{
		sublen=len	
	}
	if(x.value==".")
	{
		//alert("Only decimal point is not allowed")
		x.value=""
		x.focus();
		return false
	}
	/*else if(parseInt(sublen)>3)
	{
		alert("More than 3 digit is not allowed")
		x.focus();
		return false
	} */

	chkDecimal1(x.value,x)
	
}

function chkDecimal1(a,z)
{

	var a,x,y,i
	regInt=/^[0-9]|\.$/
	x=a.length
//	alert(a)
	count=0
	for(i=0;i<x;i++)
	{
		if(!regInt.test(a.charAt(i)))
		{ 
			//alert("Enter Only Numbers");
			//z.value=""
			z.value=z.value.substring(0,z.value.length-1)
			z.focus()
			return false
		}
		if(a.charAt(i)=='.')
		{
			count++			
			if(count==1)
				y=i
		}
	}
	if(count>1)
	{
		//alert("Only one Decimal Point is allowed")
		z.value=""
		z.focus()
		return false;
	}
	if(x-y-1>2)
	{
		//alert("Only two decimal digits are allowed")
                          z.value=z.value.substring(0,z.value.length-1)
                          event.keyCode=""
		//z.value=""
		z.focus()
		return false;
	}
	return true
}



</script>