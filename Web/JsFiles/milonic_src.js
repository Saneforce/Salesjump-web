/*
Licence to Pmcsoft Limited
*/


licenseNumber=0;
licenseURL="";
_mD=2;
_d=document;
_n=navigator;
_L=location;
_nv=$tL(_n.appVersion);
_nu=$tL(_n.userAgent);
_ps=parseInt(_n.productSub);
_f=false;
_t=true;
_n=null;
_W=window;
_wp=_W.createPopup;
ie=(_d.all)?_t:_f;
ie4=(!_d.getElementById&&ie)?_t:_f;
ie5=(!ie4&&ie&&!_wp)?_t:_f;
ie55=(!ie4&&ie&&_wp)?_t:_f;
ns6=(_nu.indexOf("gecko")!=-1)?_t:_f;
konq=(_nu.indexOf("konqueror")!=-1)?_t:_f;
sfri=(_nu.indexOf("safari")!=-1)?_t:_f;
if(konq||sfri)
{
_ps=0;
ns6=0
}
ns4=(_d.layers)?_t:_f;
ns61=(_ps>=20010726)?_t:_f;
ns7=(_ps>=20020823)?_t:_f;
op=(_W.opera)?_t:_f;
if(op||konq)ie=0;
op5=(_nu.indexOf("opera 5")!=-1)?_t:_f;
op6=(_nu.indexOf("opera 6")!=-1)?_t:_f;
op7=(_nu.indexOf("opera 7")!=-1||_nu.indexOf("opera/7")!=-1)?_t:_f;
mac=(_nv.indexOf("mac")!=-1)?_t:_f;
mac45=(_nv.indexOf("msie 4.5")!=-1)?_t:_f;
if(ns6||ns4||op||sfri)mac=_f;
ns60=_f;
IEDtD=0;
if(!op&&(_d.all&&_d.compatMode=="CSS1Compat")||(mac&&_d.doctype&&_d.doctype.name.indexOf(".dtd")!=-1))IEDtD=1;
if(op7)op=_f;
if(op)ie55=_f;
_st=0;
_en=0;
$=" ";
_m=new Array();
_mi=new Array();
_sm=new Array();
_tsm=new Array();
_cip=new Array();
$S3="2E636F6D2F";
_mn=-1;
_el=0;
_bl=0;
_MT=setTimeout("",0);
_oMT=setTimeout("",0);
_cMT=setTimeout("",0);
_mst=setTimeout("",0);
_Mtip=setTimeout("",0);
$ude="undefined ";
_zi=999;
_c=1;
_oldel=-1;
_sH=0;
_sW=0;
_bH=500;
_oldbH=0;
_bW=0;
_oldbW=0;
_ofMT=0;
_startM=1;
_sT=0;
_sL=0;
_mcnt=0;
_mnuD=0;
_itemRef=-1;
inopenmode=0;
lcl=0;
_hrF=location.href;
_hrL=_hrF.length;
_hrP=_hrF.substr((_hrF.indexOf(_L.pathname,0)),_hrL);
if(_L.pathname=="/")_hrP="/";
function M_hideLayer(){}
	for(_a=0;_a<_ar.length;_a++)
	{
		if(_ar[_a]!=_dta)
		{
			_tar[_tar.length]=_ar[_a]
		}
	}
	return _tar
}
function copyOf(_w)
{
	for(_cO in _w)
	{
		this[_cO]=_w[_cO]
	}
}
function $tL($)
{
	if($)return $.toLowerCase()
}
function $tU($)
{
	if($)return $.toUpperCase()
}
function drawMenus()
{
	_oldbH=0;
	_oldbW=0;
	for(_a=_mcnt;_a<_m.length;_a++)
	{
		_drawMenu(_a,1)
	}
}
_$S={menu:0,text:1,url:2,showmenu:3,status:4,onbgcolor:5,oncolor:6,offbgcolor:7,offcolor:8,offborder:9,separatorcolor:10,padding:11,fontsize:12,fontstyle:13,fontweight:14,fontfamily:15,high3dcolor:16,low3dcolor:17,pagecolor:18,pagebgcolor:19,headercolor:20,headerbgcolor:21,subimagepadding:22,subimageposition:23,subimage:24,onborder:25,ondecoration:26,separatorsize:27,itemheight:28,image:29,imageposition:30,imagealign:31,overimage:32,decoration:33,type:34,target:35,align:36,imageheight:37,imagewidth:38,openonclick:39,closeonclick:40,keepalive:41,onfunction:42,offfunction:43,onbold:44,onitalic:45,bgimage:46,overbgimage:47,onsubimage:48,separatorheight:49,separatorwidth:50,separatorpadding:51,separatoralign:52,onclass:53,offclass:54,itemwidth:55,pageimage:56,targetfeatures:57,visitedcolor:58,pointer:59,imagepadding:60,valign:61,clickfunction:62,bordercolor:63,borderstyle:64,borderwidth:65,overfilter:66,outfilter:67,margin:68,pagebgimage:69,swap3d:70,separatorimage:71,pageclass:72,menubgimage:73,headerborder:74,pageborder:75,title:76,pagematch:77};
function mm_style()
{
	for($i in _$S)
		this[$i]=_n;
	this.built=0
}
_$M={items:0,name:1,top:2,left:3,itemwidth:4,screenposition:5,style:6,alwaysvisible:7,align:8,orientation:9,keepalive:10,openstyle:11,margin:12,overflow:13,position:14,overfilter:15,outfilter:16,menuwidth:17,itemheight:18,followscroll:19,menualign:20,mm_callItem:21,mm_obj_ref:22,mm_built:23,menuheight:24};
{
function ami(txt)