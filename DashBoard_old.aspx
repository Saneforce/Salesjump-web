	<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="DashBoard.aspx.cs" Inherits="DashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript" src="https://cdn.fusioncharts.com/fusioncharts/latest/fusioncharts.js"></script>
    <script type="text/javascript" src="https://cdn.fusioncharts.com/fusioncharts/latest/themes/fusioncharts.theme.fusion.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="css/dashboard.css">
    <script src="js/canvasjs.min.js" type="text/javascript"></script>
    <script src="../JsFiles/amcharts.js" type="text/javascript"></script>
    <script src="../JsFiles/serial.js" type="text/javascript"></script>
    <script src="https://www.amcharts.com/lib/3/pie.js"></script>
    <script src="//www.amcharts.com/lib/3/themes/light.js"></script>
    <script type="text/javascript" src="js/highmaps.js"></script>
    <%-- <script type="text/javascript" src="js/in-all.js"></script>--%>
    <script src="js/ph-all.js" type="text/javascript"></script>
    <script src="js/mm-all.js" type="text/javascript"></script>
    <script src="js/gh-all.js" type="text/javascript"></script>
	<script src="js/sl-all.js" type="text/javascript"></script>
	<script src="js/ng-all.js" type="text/javascript"></script>
    <style type="text/css">
        canvas, .ChartTop
        {
            border-radius: 6px;
        }
        .noti
        {
            border: solid 1px #000000;
        }
        .notification_div
        {
            border-radius: 6px;
            height: 450px;
            border: solid 1px #cccccc;
            padding-left: 50px;
            margin-left: 50px;
        }
        .ChartTop
        {
            height: 180px;
            border: solid 1px #cccccc;
            width: 100%;
        }
        .Chartdown
        {
            border-radius: 6px;
            height: 188px;
            border: solid 1px #cccccc;
        }
        .ddl
        {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            font-family: Andalus;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
        }
        .huge
        {
            font-size: 20px;
        }
        .panel-green
        {
            border-color: #5cb85c;
        }
        .panel-green > .panel-heading
        {
            border-color: #5cb85c;
            color: white;
            background-color: #5cb85c;
        }
        .panel-green > a
        {
            color: #5cb85c;
        }
        .panel-green > a:hover
        {
            color: #3d8b3d;
        }
        .list-group-item
        {
            position: relative;
            padding: 10px 15px;
            margin-bottom: -1px;
            background-color: #fff;
            border: 1px solid #ddd;
        }
        
        
        #chartdiv1
        {
            width: 100%;
            height: 500px;
        }
        .bg-pink
        {
            background-color: #E91E63 !important;
            color: #fff;
        }
        
        .bg-cyan
        {
            background-color: #00BCD4 !important;
            color: #fff;
        }
        
        .bg-purble
        {
            background-color: #7266ba !important;
            color: #fff;
        }
        
        .bg-orange
        {
            background-color: #FF9800 !important;
            color: #fff;
        }
        
        
        .info-box
        {
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.2);
            height: 90px;
            display: flex;
            cursor: default;
            background-color: #fff;
            position: relative;
            overflow: hidden;
            margin-bottom: 30px;
        }
        
        .info-box.hover-expand-effect:after
        {
            background-color: rgba(0, 0, 0, 0.05);
            content: ".";
            position: absolute;
            left: 80px;
            top: 0;
            width: 0;
            height: 100%;
            color: transparent;
            -moz-transition: all 0.95s;
            -o-transition: all 0.95s;
            -webkit-transition: all 0.95s;
            transition: all 0.95s;
        }
        
        .info-box .icon
        {
            display: inline-block;
            text-align: center;
            background-color: rgba(0, 0, 0, 0.12);
            width: 70px;
        }
        
        .info-box .icon i
        {
            color: #fff;
            font-size: 50px;
            line-height: 90px;
        }
        
        .material-icons
        {
            font-family: 'Material Icons';
            font-weight: normal;
            font-style: normal;
            font-size: 24px;
            line-height: 1;
            letter-spacing: normal;
            text-transform: none;
            display: inline-block;
            white-space: nowrap;
            word-wrap: normal;
            direction: ltr;
            -webkit-font-feature-settings: 'liga';
            -webkit-font-smoothing: antialiased;
        }
        
        .info-box .content
        {
            display: inline-block;
            padding: 7px 10px;
        }
        
        .info-box .content .text
        {
            font-size: 13px;
            margin-top: 11px;
            color: #555;
        }
        
        .info-box .content .number
        {
            font-weight: normal;
            font-size: 26px;
            margin-top: -4px;
            color: #555;
        }
        
        .bg-pink .content .text, .bg-pink .content .number, .bg-cyan .content .text, .bg-cyan .content .number, .bg-purble .content .text, .bg-purble .content .number, .bg-orange .content .text, .bg-orange .content .number
        {
            color: #fff !important;
        }
        
        .content
        {
            background: none;
            overflow: hidden;
        }
        .btn
        {
            font-weight: 500;
            -webkit-border-radius: 3px;
            -moz-border-radius: 3px;
            border-radius: 3px;
            border: 1px solid transparent;
        }
        .btn.btn-default1
        {
            /* background-color: #fafafa; */
            color: #666; /* border-color: #ddd; */ /* border-bottom-color: #ddd; */
            -webkit-transition: all .3s ease;
            -moz-transition: all .3s ease;
            -ms-transition: all .3s ease;
            -o-transition: all .3s ease;
            transition: all .3s ease;
        }
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <%--<script type="text/javascript" src="JsFiles/amcharts.js"></script>
    <script type="text/javascript" src="https://www.amcharts.com/lib/3/serial.js"></script>
    <script type="text/javascript" src="https://www.amcharts.com/lib/3/pie.js"></script>
    <script type="text/javascript" src="js/highmaps.js"></script>
    <script type="text/javascript" src="js/in-all.js"></script>
    <script type="text/javascript" src="https://www.amcharts.com/lib/3/themes/light.js"></script>--%>
    <script type="text/javascript">
        Highcharts.maps["countries/in/in-all"] = { "title": "India", "version": "1.1.2", "type": "FeatureCollection", "copyright": "Copyright (c) 2015 Highsoft AS, Based on data from Natural Earth", "copyrightShort": "Natural Earth", "copyrightUrl": "http://www.naturalearthdata.com", "crs": { "type": "name", "properties": { "name": "urn:ogc:def:crs:EPSG:24373"} }, "hc-transform": { "default": { "crs": "+proj=lcc +lat_1=19 +lat_0=19 +lon_0=80 +k_0=0.99878641 +x_0=2743195.592233322 +y_0=914398.5307444407 +a=6377299.36559538 +b=6356098.359005156 +to_meter=0.9143985307444408 +no_defs", "scale": 0.000208805754963, "jsonres": 15.5, "jsonmarginX": -999, "jsonmarginY": 9851.0, "xoffset": 1676654.55745, "yoffset": 3026618.02308} },
            "features": [{ "type": "Feature", "id": "IN.PY", "properties": { "hc-group": "admin1", "hc-middle-x": 0.65, "hc-middle-y": 0.81, "hc-key": "in-py", "hc-a2": "PY", "labelrank": "2", "hasc": "IN.PY", "alt-name": "Pondicherry|Puduchcheri|PondichÃ©ry", "woe-id": "20070459", "subregion": null, "fips": "IN22", "postal-code": "PY", "name": "Puducherry", "country": "India", "type-en": "Union Territory", "region": "South", "longitude": "79.7758", "woe-name": "Puducherry", "latitude": "10.9224", "woe-label": "Puducherry, IN, India", "type": "Union Territor" }, "geometry": { "type": "MultiPolygon", "coordinates": [[[[4132, 2394], [4105, 2404], [4125, 2405], [4152, 2410], [4132, 2394]]], [[[3224, 145], [3222, 133], [3224, 78], [3169, 117], [3182, 147], [3224, 145]]], [[[3226, 529], [3214, 499], [3211, 481], [3169, 491], [3162, 547], [3226, 529]]], [[[1464, 588], [1448, 577], [1432, 581], [1433, 612], [1501, 627], [1464, 588]]]]} }, { "type": "Feature", "id": "IN.LD", "properties": { "hc-group": "admin1", "hc-middle-x": 0.59, "hc-middle-y": 0.63, "hc-key": "in-ld", "hc-a2": "LD", "labelrank": "2", "hasc": "IN.LD", "alt-name": "Ãles Laquedives|Laccadive|Minicoy and Amindivi Islands|Laccadives|Lackadiverna|Lakkadiven|Lakkadi", "woe-id": "2345748", "subregion": null, "fips": "IN14", "postal-code": "LD", "name": "Lakshadweep", "country": "India", "type-en": "Union Territory", "region": "South", "longitude": "72.7811", "woe-name": "Lakshadweep", "latitude": "11.2249", "woe-label": "Lakshadweep, IN, India", "type": "Union Territor" }, "geometry": { "type": "Polygon", "coordinates": [[[534, -879], [521, -880], [537, -871], [545, -860], [534, -879]]]} }, { "type": "Feature", "id": "IN.WB", "properties": { "hc-group": "admin1", "hc-middle-x": 0.50, "hc-middle-y": 0.74, "hc-key": "in-wb", "hc-a2": "WB", "labelrank": "2", "hasc": "IN.WB", "alt-name": "Bangla|Bengala Occidentale|Bengala Ocidental|Bengale occidental", "woe-id": "2345761", "subregion": null, "fips": "IN28", "postal-code": "WB", "name": "West Bengal", "country": "India", "type-en": "State", "region": "East", "longitude": "87.7289", "woe-name": "West Bengal", "latitude": "23.0523", "woe-label": "West Bengal, IN, India", "type": "State" }, "geometry": { "type": "MultiPolygon", "coordinates": [[[[6248, 4480], [6272, 4444], [6261, 4389], [6232, 4393], [6248, 4480]]], [[[6023, 4373], [5995, 4440], [5945, 4449], [5912, 4503], [5865, 4482], [5841, 4535], [5724, 4598], [5782, 4637], [5732, 4697], [5742, 4727], [5691, 4749], [5681, 4778], [5611, 4817], [5627, 4903], [5515, 4909], [5468, 4956], [5421, 4958], [5398, 4996], [5420, 5058], [5395, 5083], [5455, 5096], [5464, 5134], [5502, 5124], [5535, 5075], [5578, 5100], [5620, 5152], [5743, 5191], [5768, 5266], [5842, 5230], [5900, 5247], [5911, 5292], [5986, 5327], [5984, 5358], [6046, 5385], [6034, 5417], [6065, 5437], [6091, 5510], [6071, 5546], [6117, 5546], [6105, 5613], [6113, 5666], [6073, 5742], [6072, 5791], [6091, 5817], [6055, 5864], [6128, 5919], [6169, 5904], [6158, 5974], [6068, 6051], [6075, 6105], [6126, 6161], [6237, 6251], [6195, 6324], [6157, 6318], [6182, 6400], [6145, 6496], [6105, 6538], [6115, 6597], [6157, 6555], [6248, 6538], [6298, 6577], [6374, 6570], [6409, 6551], [6432, 6505], [6509, 6447], [6595, 6464], [6693, 6434], [6689, 6416], [6781, 6422], [6796, 6348], [6780, 6266], [6722, 6207], [6689, 6188], [6705, 6154], [6688, 6119], [6617, 6126], [6536, 6176], [6519, 6255], [6465, 6292], [6446, 6264], [6501, 6219], [6419, 6209], [6361, 6278], [6267, 6331], [6238, 6299], [6291, 6290], [6242, 6190], [6191, 6162], [6164, 6069], [6185, 6017], [6220, 6030], [6291, 5974], [6334, 5916], [6387, 5905], [6420, 5921], [6451, 5853], [6497, 5846], [6481, 5792], [6364, 5796], [6305, 5785], [6305, 5737], [6268, 5663], [6227, 5692], [6168, 5571], [6212, 5514], [6337, 5449], [6394, 5443], [6437, 5418], [6413, 5389], [6430, 5300], [6388, 5275], [6373, 5189], [6441, 5121], [6459, 5121], [6432, 5051], [6442, 5031], [6532, 5013], [6497, 4969], [6496, 4925], [6535, 4882], [6524, 4839], [6598, 4611], [6596, 4528], [6577, 4513], [6610, 4425], [6580, 4395], [6535, 4420], [6494, 4372], [6463, 4426], [6473, 4560], [6454, 4508], [6424, 4370], [6396, 4359], [6404, 4473], [6379, 4446], [6381, 4394], [6343, 4416], [6306, 4364], [6279, 4413], [6285, 4460], [6261, 4519], [6287, 4567], [6263, 4597], [6199, 4468], [6105, 4393], [6023, 4373]]]]} }, { "type": "Feature", "id": "IN.OR", "properties": { "hc-group": "admin1", "hc-middle-x": 0.56, "hc-middle-y": 0.38, "hc-key": "in-or", "hc-a2": "OR", "labelrank": "2", "hasc": "IN.OR", "alt-name": null, "woe-id": "2345755", "subregion": null, "fips": "IN21", "postal-code": "OR", "name": "Orissa", "country": "India", "type-en": "State", "region": "East", "longitude": "84.4341", "woe-name": "Orissa", "latitude": "20.625", "woe-label": "Orissa, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[6023, 4373], [5922, 4345], [5873, 4313], [5822, 4253], [5795, 4192], [5799, 4156], [5842, 4066], [5861, 3997], [5782, 3932], [5764, 3843], [5684, 3800], [5650, 3720], [5604, 3751], [5564, 3752], [5641, 3712], [5534, 3661], [5412, 3626], [5304, 3584], [5345, 3621], [5346, 3667], [5306, 3677], [5218, 3609], [5177, 3527], [5214, 3523], [5234, 3578], [5282, 3593], [5263, 3547], [5206, 3506], [5061, 3361], [5029, 3378], [4968, 3326], [4893, 3237], [4835, 3226], [4738, 3232], [4699, 3307], [4676, 3279], [4627, 3356], [4568, 3292], [4512, 3300], [4548, 3253], [4477, 3202], [4449, 3208], [4413, 3161], [4435, 3116], [4419, 3063], [4323, 3049], [4251, 3003], [4255, 3035], [4198, 3111], [4168, 3069], [4148, 2982], [4156, 2924], [4123, 2903], [4048, 2922], [4004, 2905], [3876, 2824], [3807, 2827], [3825, 2856], [3861, 3006], [3934, 3044], [3998, 3118], [4018, 3173], [4081, 3198], [4112, 3303], [4087, 3345], [4089, 3448], [4037, 3499], [4036, 3600], [3957, 3657], [4001, 3728], [4109, 3672], [4132, 3625], [4166, 3645], [4239, 3629], [4238, 3599], [4289, 3634], [4281, 3691], [4250, 3686], [4177, 3716], [4178, 3857], [4147, 3907], [4146, 4026], [4187, 4021], [4226, 4064], [4264, 4143], [4373, 4154], [4415, 4129], [4459, 4153], [4487, 4226], [4517, 4218], [4504, 4282], [4555, 4403], [4585, 4415], [4569, 4465], [4600, 4557], [4741, 4656], [4730, 4694], [4796, 4636], [4896, 4616], [4935, 4654], [5073, 4657], [5130, 4690], [5140, 4603], [5105, 4536], [5150, 4536], [5191, 4506], [5265, 4561], [5358, 4530], [5399, 4552], [5398, 4506], [5446, 4511], [5466, 4544], [5466, 4658], [5448, 4699], [5484, 4722], [5606, 4638], [5649, 4642], [5724, 4598], [5841, 4535], [5865, 4482], [5912, 4503], [5945, 4449], [5995, 4440], [6023, 4373]]]} }, { "type": "Feature", "id": "IN.BR", "properties": { "hc-group": "admin1", "hc-middle-x": 0.46, "hc-middle-y": 0.64, "hc-key": "in-br", "hc-a2": "BR", "labelrank": "2", "hasc": "IN.BR", "alt-name": null, "woe-id": "2345742", "subregion": null, "fips": "IN34", "postal-code": "BR", "name": "Bihar", "country": "India", "type-en": "State", "region": "East", "longitude": "85.8134", "woe-name": "Bihar", "latitude": "25.6853", "woe-label": "Bihar, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[4646, 6586], [4639, 6620], [4700, 6625], [4725, 6652], [4800, 6601], [4902, 6585], [4932, 6536], [4928, 6467], [5029, 6441], [5064, 6398], [5114, 6402], [5160, 6358], [5269, 6408], [5301, 6392], [5312, 6330], [5352, 6299], [5418, 6334], [5487, 6309], [5516, 6320], [5671, 6248], [5700, 6256], [5785, 6318], [5802, 6260], [5890, 6233], [5909, 6259], [5982, 6246], [6081, 6283], [6120, 6248], [6157, 6318], [6195, 6324], [6237, 6251], [6126, 6161], [6075, 6105], [6068, 6051], [6158, 5974], [6169, 5904], [6128, 5919], [6055, 5864], [6091, 5817], [6072, 5791], [5977, 5830], [5938, 5772], [5897, 5767], [5889, 5727], [5837, 5685], [5819, 5537], [5772, 5541], [5763, 5509], [5713, 5529], [5653, 5516], [5621, 5483], [5603, 5428], [5543, 5463], [5547, 5509], [5498, 5509], [5446, 5581], [5409, 5562], [5348, 5588], [5314, 5539], [5321, 5508], [5273, 5480], [5224, 5481], [5071, 5414], [5011, 5470], [4961, 5421], [4936, 5428], [4895, 5393], [4836, 5448], [4833, 5479], [4758, 5467], [4738, 5513], [4694, 5481], [4614, 5464], [4552, 5472], [4545, 5564], [4491, 5602], [4479, 5724], [4496, 5749], [4680, 5869], [4764, 5952], [4799, 5918], [4829, 5923], [4845, 5966], [4887, 5941], [4927, 5961], [4901, 6005], [4844, 6035], [4804, 6030], [4732, 6096], [4705, 6150], [4774, 6172], [4770, 6216], [4677, 6242], [4740, 6312], [4840, 6312], [4791, 6356], [4788, 6402], [4727, 6420], [4679, 6538], [4646, 6586]]]} }, { "type": "Feature", "id": "IN.SK", "properties": { "hc-group": "admin1", "hc-middle-x": 0.46, "hc-middle-y": 0.51, "hc-key": "in-sk", "hc-a2": "SK", "labelrank": "2", "hasc": "IN.SK", "alt-name": null, "woe-id": "2345762", "subregion": null, "fips": "IN29", "postal-code": "SK", "name": "Sikkim", "country": "India", "type-en": "State", "region": "East", "longitude": "88.4482", "woe-name": "Sikkim", "latitude": "27.5709", "woe-label": "Sikkim, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[6115, 6597], [6117, 6694], [6160, 6807], [6141, 6875], [6233, 6893], [6312, 6946], [6387, 6906], [6403, 6847], [6369, 6727], [6383, 6673], [6427, 6638], [6374, 6570], [6298, 6577], [6248, 6538], [6157, 6555], [6115, 6597]]]} }, { "type": "Feature", "id": "IN.CT", "properties": { "hc-group": "admin1", "hc-middle-x": 0.51, "hc-middle-y": 0.34, "hc-key": "in-ct", "hc-a2": "CT", "labelrank": "2", "hasc": "IN.CT", "alt-name": null, "woe-id": "20070464", "subregion": null, "fips": "IN37", "postal-code": "CT", "name": "Chhattisgarh", "country": "India", "type-en": "State", "region": "Central", "longitude": "82.3069", "woe-name": "Chhattisgarh", "latitude": "21.8044", "woe-label": "Chhattisgarh, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[4730, 4694], [4741, 4656], [4600, 4557], [4569, 4465], [4585, 4415], [4555, 4403], [4504, 4282], [4517, 4218], [4487, 4226], [4459, 4153], [4415, 4129], [4373, 4154], [4264, 4143], [4226, 4064], [4187, 4021], [4146, 4026], [4147, 3907], [4178, 3857], [4177, 3716], [4250, 3686], [4281, 3691], [4289, 3634], [4238, 3599], [4239, 3629], [4166, 3645], [4132, 3625], [4109, 3672], [4001, 3728], [3957, 3657], [4036, 3600], [4037, 3499], [4089, 3448], [4087, 3345], [4112, 3303], [4081, 3198], [4018, 3173], [3998, 3118], [3934, 3044], [3861, 3006], [3825, 2856], [3807, 2827], [3714, 2838], [3676, 2814], [3653, 2839], [3635, 2935], [3573, 2992], [3544, 3062], [3471, 3136], [3415, 3132], [3385, 3180], [3412, 3234], [3385, 3284], [3435, 3390], [3498, 3446], [3553, 3396], [3593, 3428], [3616, 3482], [3548, 3524], [3483, 3609], [3458, 3592], [3441, 3641], [3481, 3652], [3480, 3736], [3442, 3741], [3441, 3775], [3514, 3811], [3513, 3919], [3467, 3917], [3495, 3957], [3487, 4043], [3444, 4075], [3462, 4141], [3514, 4170], [3533, 4213], [3549, 4261], [3551, 4355], [3591, 4407], [3589, 4448], [3620, 4506], [3641, 4488], [3658, 4544], [3692, 4583], [3713, 4655], [3774, 4663], [3801, 4640], [3882, 4685], [3927, 4730], [3934, 4804], [3989, 4841], [3991, 4879], [4057, 4901], [4078, 4977], [4006, 5020], [3982, 5057], [3916, 5075], [3879, 5051], [3860, 5082], [3894, 5133], [3869, 5202], [3927, 5177], [3984, 5193], [4035, 5178], [4199, 5170], [4284, 5231], [4295, 5259], [4355, 5227], [4442, 5240], [4488, 5318], [4555, 5290], [4569, 5239], [4620, 5194], [4652, 5104], [4698, 5096], [4723, 5128], [4737, 5093], [4714, 5020], [4739, 5025], [4746, 4933], [4779, 4876], [4857, 4866], [4866, 4833], [4809, 4759], [4730, 4694]]]} }, { "type": "Feature", "id": "IN.TN", "properties": { "hc-group": "admin1", "hc-middle-x": 0.59, "hc-middle-y": 0.41, "hc-key": "in-tn", "hc-a2": "TN", "labelrank": "2", "hasc": "IN.", "alt-name": null, "woe-id": "2345758", "subregion": null, "fips": "IN22", "postal-code": "TN", "name": "Tamil Nadu", "country": "India", "type-en": "State", "region": "South", "longitude": "78.2704", "woe-name": "Tamil Nadu", "latitude": "11.0159", "woe-label": "Tamil Nadu, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[3226, 529], [3162, 547], [3169, 491], [3211, 481], [3188, 380], [3201, 321], [3166, 273], [3219, 288], [3224, 145], [3182, 147], [3169, 117], [3224, 78], [3226, -131], [3122, -106], [3048, -114], [2985, -176], [3001, -228], [2970, -254], [2883, -373], [2856, -449], [2906, -513], [2989, -524], [3016, -510], [3057, -575], [3005, -539], [2873, -529], [2740, -581], [2673, -590], [2589, -649], [2539, -771], [2543, -838], [2517, -884], [2297, -999], [2220, -977], [2139, -913], [2161, -905], [2208, -819], [2169, -751], [2198, -686], [2166, -629], [2202, -582], [2261, -433], [2238, -401], [2178, -387], [2209, -249], [2196, -211], [2217, -151], [2185, -95], [2103, -141], [2047, -109], [2042, 19], [2069, 77], [2043, 115], [1993, 139], [2023, 185], [1976, 248], [1898, 251], [1938, 299], [1887, 339], [1828, 357], [1823, 407], [1861, 401], [1892, 430], [1937, 446], [1944, 420], [2049, 411], [2084, 479], [2119, 488], [2151, 461], [2220, 483], [2288, 480], [2310, 525], [2381, 542], [2419, 600], [2381, 633], [2313, 635], [2364, 743], [2344, 774], [2361, 818], [2396, 821], [2436, 890], [2534, 884], [2600, 850], [2673, 799], [2737, 856], [2761, 939], [2790, 963], [2877, 979], [2961, 959], [2995, 999], [3061, 1027], [3069, 1067], [3156, 1055], [3201, 1029], [3213, 1057], [3322, 1123], [3384, 1125], [3402, 1114], [3391, 1142], [3391, 1143], [3395, 1148], [3402, 1114], [3410, 1110], [3412, 1019], [3394, 967], [3372, 813], [3344, 734], [3230, 556], [3226, 529]]]} }, { "type": "Feature", "id": "IN.MP", "properties": { "hc-group": "admin1", "hc-middle-x": 0.47, "hc-middle-y": 0.60, "hc-key": "in-mp", "hc-a2": "MP", "labelrank": "2", "hasc": "IN.MP", "alt-name": null, "woe-id": "2345749", "subregion": null, "fips": "IN35", "postal-code": "MP", "name": "Madhya Pradesh", "country": "India", "type-en": "State", "region": "Central", "longitude": "78.42140000000001", "woe-name": "Madhya Pradesh", "latitude": "22.9404", "woe-label": "Madhya Pradesh, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[4295, 5259], [4284, 5231], [4199, 5170], [4035, 5178], [3984, 5193], [3927, 5177], [3869, 5202], [3894, 5133], [3860, 5082], [3879, 5051], [3916, 5075], [3982, 5057], [4006, 5020], [4078, 4977], [4057, 4901], [3991, 4879], [3989, 4841], [3934, 4804], [3927, 4730], [3882, 4685], [3801, 4640], [3774, 4663], [3713, 4655], [3692, 4583], [3658, 4544], [3641, 4488], [3620, 4506], [3589, 4448], [3591, 4407], [3551, 4355], [3549, 4261], [3533, 4213], [3434, 4228], [3435, 4258], [3352, 4325], [3286, 4289], [3225, 4284], [3210, 4305], [3120, 4290], [3069, 4345], [3006, 4354], [2997, 4332], [2889, 4300], [2882, 4275], [2806, 4262], [2708, 4276], [2685, 4322], [2561, 4250], [2445, 4229], [2363, 4235], [2343, 4277], [2388, 4293], [2388, 4354], [2355, 4383], [2308, 4385], [2266, 4357], [2228, 4367], [2109, 4311], [2084, 4248], [2049, 4220], [2040, 4166], [1975, 4130], [1883, 4123], [1847, 4235], [1772, 4249], [1646, 4241], [1564, 4247], [1498, 4271], [1469, 4317], [1414, 4342], [1346, 4343], [1275, 4391], [1268, 4477], [1241, 4508], [1165, 4472], [1138, 4483], [1144, 4530], [1120, 4580], [1122, 4638], [1178, 4638], [1107, 4691], [1157, 4704], [1237, 4754], [1269, 4814], [1236, 4842], [1223, 4906], [1278, 4915], [1367, 4958], [1303, 4988], [1316, 5033], [1409, 5083], [1453, 5155], [1445, 5237], [1468, 5290], [1435, 5371], [1386, 5382], [1429, 5450], [1381, 5486], [1409, 5542], [1474, 5550], [1479, 5586], [1430, 5592], [1426, 5645], [1482, 5616], [1519, 5626], [1538, 5685], [1607, 5690], [1596, 5623], [1559, 5627], [1560, 5560], [1648, 5549], [1760, 5575], [1800, 5471], [1748, 5437], [1771, 5390], [1751, 5345], [1771, 5316], [1742, 5270], [1674, 5288], [1655, 5249], [1693, 5200], [1730, 5189], [1758, 5224], [1830, 5250], [1840, 5288], [1883, 5319], [1891, 5396], [1925, 5375], [2007, 5368], [2038, 5348], [2077, 5384], [2118, 5325], [2163, 5356], [2137, 5403], [2135, 5480], [2170, 5457], [2226, 5481], [2194, 5551], [2128, 5591], [2165, 5614], [2158, 5668], [2276, 5703], [2331, 5697], [2352, 5735], [2328, 5827], [2275, 5788], [2179, 5776], [2086, 5805], [2050, 5838], [2026, 5941], [2051, 5991], [2116, 6017], [2182, 6087], [2291, 6142], [2332, 6185], [2370, 6193], [2514, 6271], [2539, 6306], [2602, 6312], [2610, 6357], [2646, 6367], [2706, 6378], [2774, 6341], [2826, 6356], [2903, 6319], [2950, 6180], [2915, 6150], [2911, 6083], [2859, 5978], [2834, 5958], [2825, 5893], [2701, 5865], [2662, 5810], [2699, 5721], [2687, 5657], [2627, 5595], [2659, 5527], [2647, 5464], [2690, 5415], [2702, 5372], [2740, 5415], [2848, 5338], [2916, 5436], [2873, 5504], [2828, 5503], [2829, 5573], [2796, 5632], [2786, 5700], [2752, 5752], [2782, 5810], [2832, 5785], [2829, 5841], [2855, 5814], [2891, 5857], [2908, 5820], [2887, 5715], [2941, 5749], [2934, 5714], [3022, 5710], [3031, 5747], [3078, 5746], [3047, 5703], [3066, 5674], [3120, 5714], [3235, 5705], [3231, 5742], [3352, 5808], [3385, 5808], [3435, 5716], [3387, 5651], [3469, 5661], [3448, 5691], [3492, 5703], [3516, 5673], [3530, 5707], [3592, 5719], [3563, 5634], [3645, 5633], [3678, 5610], [3709, 5635], [3730, 5713], [3835, 5726], [3852, 5679], [3936, 5662], [3959, 5617], [4098, 5565], [4114, 5505], [4156, 5540], [4281, 5522], [4268, 5476], [4281, 5411], [4269, 5343], [4248, 5330], [4295, 5259]]]} }, { "type": "Feature", "id": "IN.2984", "properties": { "hc-group": "admin1", "hc-middle-x": 0.56, "hc-middle-y": 0.32, "hc-key": "in-2984", "hc-a2": "GU", "labelrank": "2", "hasc": "IN.2984", "alt-name": null, "woe-id": "2345743", "subregion": null, "fips": "IN32", "postal-code": "2984", "name": "Gujarat", "country": "India", "type-en": null, "region": "West", "longitude": "71.3013", "woe-name": "Gujarat", "latitude": "22.7501", "woe-label": "Gujarat, IN, India", "type": null }, "geometry": { "type": "Polygon", "coordinates": [[[1223, 4906], [1236, 4842], [1269, 4814], [1237, 4754], [1157, 4704], [1107, 4691], [1178, 4638], [1122, 4638], [1120, 4580], [1144, 4530], [1138, 4483], [1020, 4442], [1024, 4369], [995, 4355], [1019, 4314], [1084, 4330], [1192, 4310], [1069, 4267], [1052, 4227], [1008, 4225], [968, 4164], [1029, 4104], [1042, 4026], [996, 3961], [936, 3946], [873, 4009], [840, 3981], [872, 3939], [834, 3874], [832, 3799], [798, 3806], [758, 3777], [724, 3866], [660, 3823], [677, 3785], [612, 3777], [588, 3767], [606, 3848], [634, 3886], [658, 3890], [646, 3920], [669, 4023], [659, 4088], [602, 4160], [611, 4208], [570, 4236], [557, 4283], [579, 4322], [695, 4395], [581, 4400], [551, 4412], [588, 4510], [544, 4515], [562, 4586], [596, 4605], [637, 4585], [674, 4614], [568, 4635], [508, 4626], [512, 4670], [477, 4618], [475, 4559], [407, 4524], [420, 4468], [360, 4477], [365, 4444], [418, 4449], [463, 4393], [440, 4326], [387, 4263], [385, 4220], [143, 4106], [-27, 4056], [-36, 4057], [-40, 4046], [-89, 4046], [-197, 4093], [-269, 4144], [-367, 4236], [-450, 4353], [-513, 4428], [-662, 4580], [-747, 4709], [-733, 4751], [-649, 4743], [-650, 4694], [-549, 4717], [-524, 4757], [-501, 4718], [-455, 4764], [-437, 4740], [-371, 4781], [-292, 4787], [-213, 4916], [-155, 4960], [-175, 5003], [-201, 4981], [-203, 4932], [-262, 4957], [-274, 4939], [-410, 4910], [-456, 4869], [-627, 4916], [-757, 4994], [-841, 5067], [-844, 5120], [-907, 5172], [-883, 5197], [-901, 5244], [-752, 5332], [-821, 5318], [-905, 5272], [-938, 5229], [-999, 5239], [-955, 5261], [-991, 5296], [-983, 5333], [-891, 5376], [-785, 5368], [-776, 5495], [-716, 5503], [-692, 5477], [-577, 5481], [-476, 5472], [-439, 5435], [-331, 5419], [-278, 5471], [-128, 5510], [-116, 5455], [-47, 5430], [14, 5478], [74, 5494], [37, 5517], [32, 5565], [70, 5605], [139, 5580], [246, 5595], [390, 5578], [428, 5594], [556, 5514], [566, 5481], [606, 5507], [672, 5455], [736, 5445], [765, 5495], [794, 5500], [812, 5432], [789, 5381], [846, 5304], [887, 5338], [909, 5273], [885, 5212], [975, 5129], [976, 5073], [1042, 5069], [1068, 5030], [1095, 5037], [1191, 4954], [1223, 4906]]]} }, { "type": "Feature", "id": "IN.GA", "properties": { "hc-group": "admin1", "hc-middle-x": 0.60, "hc-middle-y": 0.47, "hc-key": "in-ga", "hc-a2": "GA", "labelrank": "2", "hasc": "IN.GA", "alt-name": "GÃ´a", "woe-id": "2345764", "subregion": null, "fips": "IN08", "postal-code": "GA", "name": "Goa", "country": "India", "type-en": "State", "region": "West", "longitude": "73.99509999999999", "woe-name": "Goa", "latitude": "15.3133", "woe-label": "Goa, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[1031, 1720], [966, 1797], [980, 1828], [961, 1901], [927, 1943], [894, 2052], [943, 2074], [985, 2013], [1045, 2014], [1092, 2009], [1115, 1868], [1089, 1744], [1031, 1720]]]} }, { "type": "Feature", "id": "IN.NL", "properties": { "hc-group": "admin1", "hc-middle-x": 0.63, "hc-middle-y": 0.55, "hc-key": "in-nl", "hc-a2": "NL", "labelrank": "2", "hasc": "IN.NL", "alt-name": null, "woe-id": "2345754", "subregion": null, "fips": "IN20", "postal-code": "NL", "name": "Nagaland", "country": "India", "type-en": "State", "region": "Northeast", "longitude": "94.5664", "woe-name": "Nagaland", "latitude": "26.1094", "woe-label": "Nagaland, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[8692, 6530], [8648, 6508], [8624, 6455], [8634, 6366], [8678, 6291], [8629, 6236], [8638, 6164], [8559, 6060], [8520, 6039], [8477, 6062], [8485, 6130], [8402, 6055], [8361, 6045], [8294, 6063], [8215, 6052], [8220, 6014], [8155, 5914], [8096, 5948], [8096, 5948], [8096, 5948], [8096, 5960], [8097, 5979], [8088, 5990], [8048, 6037], [8203, 6209], [8197, 6165], [8241, 6178], [8269, 6216], [8268, 6310], [8322, 6389], [8349, 6463], [8382, 6444], [8422, 6518], [8492, 6549], [8565, 6639], [8606, 6635], [8654, 6679], [8668, 6614], [8692, 6530]]]} }, { "type": "Feature", "id": "IN.MN", "properties": { "hc-group": "admin1", "hc-middle-x": 0.51, "hc-middle-y": 0.48, "hc-key": "in-mn", "hc-a2": "MN", "labelrank": "2", "hasc": "IN.MN", "alt-name": null, "woe-id": "2345751", "subregion": null, "fips": "IN17", "postal-code": "MN", "name": "Manipur", "country": "India", "type-en": "State", "region": "Northeast", "longitude": "93.84569999999999", "woe-name": "Manipur", "latitude": "24.7442", "woe-label": "Manipur, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[8096, 5948], [8155, 5914], [8220, 6014], [8215, 6052], [8294, 6063], [8361, 6045], [8402, 6055], [8485, 6130], [8477, 6062], [8520, 6039], [8491, 5957], [8544, 5919], [8555, 5875], [8521, 5762], [8496, 5744], [8428, 5585], [8402, 5456], [8374, 5394], [8335, 5420], [8265, 5422], [8193, 5445], [8139, 5421], [8104, 5465], [8081, 5447], [8010, 5445], [7969, 5462], [7968, 5582], [7993, 5654], [7994, 5746], [8018, 5748], [8078, 5920], [8096, 5948], [8096, 5948], [8096, 5948]]]} }, { "type": "Feature", "id": "IN.AR", "properties": { "hc-group": "admin1", "hc-middle-x": 0.53, "hc-middle-y": 0.38, "hc-key": "in-ar", "hc-a2": "AR", "labelrank": "2", "hasc": "IN.AR", "alt-name": "Agence de la Frontisre du Nord-Est(French-obsolete)|North East Frontier Agency", "woe-id": "2345763", "subregion": null, "fips": "IN30", "postal-code": "AR", "name": "Arunachal Pradesh", "country": "India", "type-en": "State", "region": "Northeast", "longitude": "94.46729999999999", "woe-name": "Arunachal Pradesh", "latitude": "28.4056", "woe-label": "Arunachal Pradesh, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[7563, 6550], [7527, 6641], [7554, 6701], [7511, 6765], [7433, 6737], [7375, 6785], [7383, 6870], [7504, 6865], [7536, 6894], [7618, 6897], [7729, 6956], [7751, 7006], [7733, 7032], [7799, 7071], [7879, 7147], [7897, 7199], [7993, 7277], [8056, 7282], [8227, 7385], [8297, 7441], [8267, 7468], [8308, 7515], [8353, 7529], [8385, 7567], [8441, 7510], [8517, 7500], [8503, 7517], [8621, 7480], [8627, 7514], [8698, 7519], [8722, 7568], [8767, 7559], [8772, 7609], [8909, 7632], [8935, 7590], [8980, 7603], [8977, 7560], [8930, 7530], [8940, 7499], [8992, 7531], [9081, 7424], [9092, 7386], [9039, 7342], [9067, 7269], [9083, 7335], [9132, 7342], [9235, 7248], [9286, 7268], [9365, 7216], [9359, 7166], [9387, 7129], [9381, 7089], [9342, 7087], [9229, 6964], [9249, 6882], [9341, 6779], [9304, 6762], [9235, 6794], [9200, 6851], [9139, 6850], [9115, 6823], [8986, 6798], [8943, 6767], [8905, 6699], [8867, 6682], [8807, 6609], [8772, 6601], [8742, 6551], [8692, 6530], [8668, 6614], [8654, 6679], [8737, 6729], [8776, 6785], [8812, 6771], [8902, 6797], [8935, 6836], [8922, 6868], [8893, 6846], [8886, 6907], [8841, 6967], [8897, 7082], [8810, 7073], [8753, 7033], [8672, 7023], [8388, 6879], [8304, 6894], [8308, 6846], [8179, 6705], [8175, 6664], [8131, 6625], [8050, 6595], [8019, 6610], [7895, 6586], [7819, 6619], [7609, 6549], [7563, 6550]]]} }, { "type": "Feature", "id": "IN.MZ", "properties": { "hc-group": "admin1", "hc-middle-x": 0.53, "hc-middle-y": 0.39, "hc-key": "in-mz", "hc-a2": "MZ", "labelrank": "2", "hasc": "IN.MZ", "alt-name": null, "woe-id": "20070461", "subregion": null, "fips": "IN31", "postal-code": "MZ", "name": "Mizoram", "country": "India", "type-en": "State", "region": "Northeast", "longitude": "92.84090000000001", "woe-name": "Mizoram", "latitude": "23.2037", "woe-label": "Mizoram, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[8081, 5447], [8116, 5332], [8139, 5298], [8134, 5094], [8110, 5048], [8052, 5039], [8069, 4991], [8038, 4946], [8052, 4845], [8080, 4798], [8077, 4719], [8034, 4721], [8024, 4639], [7977, 4647], [7912, 4694], [7903, 4640], [7877, 4622], [7872, 4697], [7828, 4901], [7769, 5001], [7754, 5083], [7761, 5129], [7715, 5252], [7715, 5289], [7732, 5348], [7725, 5481], [7714, 5500], [7756, 5503], [7773, 5462], [7831, 5508], [7869, 5603], [7901, 5568], [7968, 5582], [7969, 5462], [8010, 5445], [8081, 5447]]]} }, { "type": "Feature", "id": "IN.TR", "properties": { "hc-group": "admin1", "hc-middle-x": 0.51, "hc-middle-y": 0.46, "hc-key": "in-tr", "hc-a2": "TR", "labelrank": "2", "hasc": "IN.TR", "alt-name": null, "woe-id": "2345759", "subregion": null, "fips": "IN26", "postal-code": "TR", "name": "Tripura", "country": "India", "type-en": "State", "region": "Northeast", "longitude": "91.70310000000001", "woe-name": "Tripura", "latitude": "23.8519", "woe-label": "Tripura, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[7714, 5500], [7725, 5481], [7732, 5348], [7715, 5289], [7675, 5298], [7624, 5261], [7598, 5289], [7605, 5200], [7555, 5146], [7540, 5108], [7563, 5037], [7491, 4975], [7441, 5066], [7406, 5095], [7410, 5017], [7383, 5046], [7348, 5172], [7313, 5227], [7333, 5344], [7375, 5381], [7381, 5423], [7452, 5424], [7473, 5470], [7511, 5484], [7560, 5455], [7576, 5532], [7641, 5561], [7652, 5615], [7695, 5561], [7685, 5508], [7714, 5500]]]} }, { "type": "Feature", "id": "IN.3464", "properties": { "hc-group": "admin1", "hc-middle-x": 0.98, "hc-middle-y": 0.91, "hc-key": "in-3464", "hc-a2": "DA", "labelrank": "2", "hasc": "IN.", "alt-name": null, "woe-id": "20070460", "subregion": null, "fips": "IN32", "postal-code": null, "name": "Daman and Diu", "country": "India", "type-en": null, "region": "West", "longitude": "72.8511", "woe-name": "Daman and Diu", "latitude": "20.4226", "woe-label": "Daman and Diu, IN, India", "type": null }, "geometry": { "type": "MultiPolygon", "coordinates": [[[[634, 3886], [639, 3909], [646, 3920], [658, 3890], [634, 3886]]], [[[-27, 4056], [-29, 4051], [-40, 4046], [-36, 4057], [-27, 4056]]]]} }, { "type": "Feature", "id": "IN.DL", "properties": { "hc-group": "admin1", "hc-middle-x": 0.60, "hc-middle-y": 0.46, "hc-key": "in-dl", "hc-a2": "DL", "labelrank": "9", "hasc": "IN.DL", "alt-name": null, "woe-id": "20070458", "subregion": null, "fips": "IN07", "postal-code": "DL", "name": "Delhi", "country": "India", "type-en": "Union Territory", "region": "Central", "longitude": "77.0856", "woe-name": "Delhi", "latitude": "28.69", "woe-label": "Delhi, IN, India", "type": "Union Territor" }, "geometry": { "type": "Polygon", "coordinates": [[[2346, 7043], [2299, 7010], [2256, 7048], [2187, 7046], [2174, 7067], [2214, 7116], [2211, 7163], [2258, 7189], [2304, 7186], [2340, 7123], [2346, 7043]]]} }, { "type": "Feature", "id": "IN.HR", "properties": { "hc-group": "admin1", "hc-middle-x": 0.55, "hc-middle-y": 0.62, "hc-key": "in-hr", "hc-a2": "HR", "labelrank": "2", "hasc": "IN.HR", "alt-name": null, "woe-id": "2345744", "subregion": null, "fips": "IN10", "postal-code": "HR", "name": "Haryana", "country": "India", "type-en": "State", "region": "Central", "longitude": "76.271", "woe-name": "Haryana", "latitude": "29.1003", "woe-label": "Haryana, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[2304, 7186], [2258, 7189], [2211, 7163], [2214, 7116], [2174, 7067], [2187, 7046], [2256, 7048], [2299, 7010], [2346, 7043], [2405, 6992], [2415, 6937], [2393, 6855], [2414, 6817], [2325, 6762], [2241, 6762], [2251, 6736], [2194, 6709], [2206, 6781], [2208, 6888], [2179, 6931], [2104, 6875], [2070, 6831], [2035, 6866], [2037, 6907], [1942, 6874], [1932, 6853], [1950, 6782], [1874, 6784], [1853, 6829], [1886, 6879], [1856, 6893], [1906, 6915], [1840, 7007], [1803, 7018], [1739, 7105], [1693, 7257], [1653, 7367], [1566, 7345], [1477, 7414], [1404, 7389], [1379, 7433], [1402, 7444], [1402, 7563], [1358, 7554], [1387, 7600], [1378, 7635], [1424, 7620], [1485, 7646], [1544, 7600], [1571, 7606], [1627, 7548], [1603, 7525], [1623, 7474], [1699, 7569], [1754, 7551], [1818, 7576], [1877, 7538], [1967, 7584], [1955, 7622], [1975, 7695], [2027, 7687], [2033, 7712], [2079, 7671], [2117, 7719], [2081, 7732], [2145, 7771], [2148, 7801], [2211, 7789], [2211, 7875], [2186, 7908], [2186, 7930], [2183, 7951], [2189, 7973], [2168, 7999], [2215, 7995], [2247, 7946], [2286, 7923], [2290, 7861], [2315, 7834], [2447, 7785], [2441, 7743], [2321, 7624], [2303, 7556], [2259, 7457], [2290, 7396], [2285, 7287], [2304, 7186]]]} }, { "type": "Feature", "id": "IN.CH", "properties": { "hc-group": "admin1", "hc-middle-x": 0.73, "hc-middle-y": 0.29, "hc-key": "in-ch", "hc-a2": "CH", "labelrank": "9", "hasc": "IN.CH", "alt-name": null, "woe-id": "20070456", "subregion": null, "fips": "IN05", "postal-code": "CH", "name": "Chandigarh", "country": "India", "type-en": "Union Territory", "region": "North", "longitude": "76.76049999999999", "woe-name": "Chandigarh", "latitude": "30.7452", "woe-label": "Chandigarh, IN, India", "type": "Union Territor" }, "geometry": { "type": "Polygon", "coordinates": [[[2183, 7951], [2186, 7930], [2186, 7908], [2144, 7949], [2183, 7951]]]} }, { "type": "Feature", "id": "IN.HP", "properties": { "hc-group": "admin1", "hc-middle-x": 0.36, "hc-middle-y": 0.41, "hc-key": "in-hp", "hc-a2": "HP", "labelrank": "2", "hasc": "IN.HP", "alt-name": null, "woe-id": "2345745", "subregion": null, "fips": "IN11", "postal-code": "HP", "name": "Himachal Pradesh", "country": "India", "type-en": "Union Territory", "region": "North", "longitude": "77.28749999999999", "woe-name": "Himachal Pradesh", "latitude": "31.6755", "woe-label": "Himachal Pradesh, IN, India", "type": "Union Territor" }, "geometry": { "type": "Polygon", "coordinates": [[[2447, 7785], [2315, 7834], [2290, 7861], [2286, 7923], [2247, 7946], [2215, 7995], [2168, 7999], [2126, 8037], [2106, 8130], [2055, 8166], [2035, 8207], [2001, 8164], [1961, 8176], [1932, 8268], [1884, 8366], [1898, 8383], [1871, 8433], [1770, 8484], [1809, 8555], [1886, 8618], [1865, 8653], [1891, 8693], [1888, 8755], [1854, 8807], [1911, 8811], [1995, 8866], [2023, 8904], [2113, 8936], [2175, 8921], [2247, 8850], [2308, 8809], [2380, 8784], [2445, 8804], [2508, 8841], [2554, 8784], [2604, 8678], [2727, 8739], [2729, 8692], [2693, 8634], [2731, 8643], [2755, 8598], [2756, 8522], [2853, 8415], [2827, 8337], [2878, 8271], [2836, 8232], [2858, 8206], [2849, 8162], [2898, 8147], [2937, 8079], [2894, 8076], [2868, 8110], [2767, 8116], [2693, 8145], [2608, 8099], [2574, 8099], [2507, 8019], [2520, 8003], [2491, 7948], [2526, 7839], [2440, 7798], [2447, 7785]]]} }, { "type": "Feature", "id": "IN.JK", "properties": { "hc-group": "admin1", "hc-middle-x": 0.54, "hc-middle-y": 0.45, "hc-key": "in-jk", "hc-a2": "JK", "labelrank": "2", "hasc": "IN.JK", "alt-name": null, "woe-id": "2345746", "subregion": null, "fips": "IN12", "postal-code": "JK", "name": "Jammu and Kashmir", "country": "India", "type-en": "State", "region": "North", "longitude": "76.6395", "woe-name": "Jammu and Kashmir", "latitude": "33.9658", "woe-label": "Jammu and Kashmir, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[2731, 8643], [2693, 8634], [2729, 8692], [2727, 8739], [2604, 8678], [2554, 8784], [2508, 8841], [2445, 8804], [2380, 8784], [2308, 8809], [2247, 8850], [2175, 8921], [2113, 8936], [2023, 8904], [1995, 8866], [1911, 8811], [1854, 8807], [1888, 8755], [1891, 8693], [1865, 8653], [1745, 8568], [1723, 8593], [1675, 8583], [1583, 8640], [1481, 8640], [1449, 8693], [1471, 8790], [1414, 8757], [1342, 8786], [1349, 8851], [1293, 8880], [1243, 8938], [1244, 8975], [1281, 9004], [1296, 9087], [1241, 9129], [1263, 9201], [1300, 9206], [1346, 9253], [1331, 9274], [1236, 9276], [1216, 9307], [1256, 9351], [1237, 9395], [1183, 9424], [1237, 9502], [1236, 9533], [1361, 9581], [1403, 9578], [1487, 9544], [1605, 9516], [1671, 9521], [1716, 9484], [1804, 9458], [1860, 9458], [1945, 9527], [2025, 9528], [2072, 9554], [2123, 9543], [2188, 9580], [2202, 9625], [2249, 9620], [2280, 9647], [2291, 9696], [2547, 9851], [2575, 9824], [2609, 9850], [2633, 9833], [2613, 9757], [2656, 9670], [2702, 9507], [2738, 9474], [2814, 9461], [2920, 9389], [2938, 9326], [2855, 9270], [2851, 9236], [2875, 9134], [2870, 9056], [2935, 8957], [3073, 8898], [3057, 8814], [3113, 8729], [3105, 8688], [3036, 8626], [2981, 8619], [2968, 8578], [2911, 8572], [2864, 8615], [2845, 8686], [2816, 8662], [2731, 8643]]]} }, { "type": "Feature", "id": "IN.KL", "properties": { "hc-group": "admin1", "hc-middle-x": 0.75, "hc-middle-y": 0.64, "hc-key": "in-kl", "hc-a2": "KL", "labelrank": "2", "hasc": "IN.KL", "alt-name": null, "woe-id": "2345747", "subregion": null, "fips": "IN13", "postal-code": "KL", "name": "Kerala", "country": "India", "type-en": "State", "region": "South", "longitude": "76.52370000000001", "woe-name": "Kerala", "latitude": "10.3666", "woe-label": "Kerala, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[1432, 581], [1334, 787], [1298, 877], [1333, 880], [1395, 821], [1457, 808], [1499, 687], [1590, 608], [1641, 594], [1673, 547], [1754, 550], [1765, 513], [1800, 507], [1892, 430], [1861, 401], [1823, 407], [1828, 357], [1887, 339], [1938, 299], [1898, 251], [1976, 248], [2023, 185], [1993, 139], [2043, 115], [2069, 77], [2042, 19], [2047, -109], [2103, -141], [2185, -95], [2217, -151], [2196, -211], [2209, -249], [2178, -387], [2238, -401], [2261, -433], [2202, -582], [2166, -629], [2198, -686], [2169, -751], [2208, -819], [2161, -905], [2139, -913], [2095, -880], [1926, -669], [1852, -495], [1833, -428], [1816, -273], [1875, -354], [1855, -261], [1810, -232], [1740, -22], [1692, 79], [1660, 211], [1609, 339], [1579, 361], [1548, 448], [1464, 545], [1464, 588], [1501, 627], [1433, 612], [1432, 581]]]} }, { "type": "Feature", "id": "IN.KA", "properties": { "hc-group": "admin1", "hc-middle-x": 0.37, "hc-middle-y": 0.48, "hc-key": "in-ka", "hc-a2": "KA", "labelrank": "2", "hasc": "IN.KA", "alt-name": "Maisur|Mysore", "woe-id": "2345753", "subregion": null, "fips": "IN19", "postal-code": "KA", "name": "Karnataka", "country": "India", "type-en": "State", "region": "South", "longitude": "75.667", "woe-name": "Karnataka", "latitude": "14.3681", "woe-label": "Karnataka, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[1892, 430], [1800, 507], [1765, 513], [1754, 550], [1673, 547], [1641, 594], [1590, 608], [1499, 687], [1457, 808], [1395, 821], [1333, 880], [1298, 877], [1285, 916], [1238, 1179], [1207, 1328], [1178, 1370], [1139, 1549], [1106, 1576], [1094, 1644], [1032, 1683], [1031, 1720], [1089, 1744], [1115, 1868], [1092, 2009], [1045, 2014], [1079, 2062], [1134, 2059], [1191, 2194], [1186, 2250], [1141, 2262], [1148, 2304], [1110, 2365], [1152, 2365], [1182, 2403], [1225, 2367], [1271, 2386], [1279, 2425], [1345, 2443], [1341, 2480], [1386, 2517], [1462, 2483], [1497, 2517], [1633, 2520], [1646, 2565], [1613, 2683], [1637, 2714], [1700, 2675], [1733, 2690], [1757, 2654], [1860, 2677], [1910, 2661], [1904, 2765], [1977, 2825], [2037, 2798], [2118, 2901], [2122, 2967], [2195, 2987], [2249, 3039], [2277, 3088], [2326, 3018], [2369, 3027], [2392, 2878], [2347, 2834], [2356, 2808], [2313, 2763], [2383, 2729], [2415, 2737], [2296, 2604], [2330, 2533], [2307, 2407], [2315, 2357], [2248, 2302], [2336, 2273], [2331, 2125], [2316, 2106], [2202, 2111], [2160, 2078], [2178, 1996], [2132, 1932], [2153, 1873], [2183, 1856], [2190, 1787], [2151, 1738], [2052, 1774], [2034, 1733], [2073, 1706], [2041, 1607], [2044, 1567], [2111, 1520], [2084, 1498], [2101, 1445], [2153, 1434], [2190, 1477], [2235, 1473], [2314, 1416], [2262, 1392], [2287, 1300], [2245, 1346], [2177, 1345], [2133, 1369], [2123, 1411], [2085, 1405], [2137, 1306], [2117, 1258], [2184, 1258], [2189, 1307], [2280, 1281], [2302, 1224], [2361, 1239], [2437, 1291], [2437, 1316], [2493, 1324], [2486, 1272], [2529, 1292], [2552, 1218], [2586, 1181], [2657, 1176], [2649, 1091], [2731, 1029], [2673, 923], [2606, 886], [2600, 850], [2534, 884], [2436, 890], [2396, 821], [2361, 818], [2344, 774], [2364, 743], [2313, 635], [2381, 633], [2419, 600], [2381, 542], [2310, 525], [2288, 480], [2220, 483], [2151, 461], [2119, 488], [2084, 479], [2049, 411], [1944, 420], [1937, 446], [1892, 430]]]} }, { "type": "Feature", "id": "IN.DN", "properties": { "hc-group": "admin1", "hc-middle-x": 0.51, "hc-middle-y": 0.59, "hc-key": "in-dn", "hc-a2": "DN", "labelrank": "9", "hasc": "IN.DN", "alt-name": "DAdra et Nagar Haveli|Dadra e Nagar Haveli", "woe-id": "20070457", "subregion": null, "fips": "IN06", "postal-code": "DN", "name": "Dadra and Nagar Haveli", "country": "India", "type-en": "Union Territory", "region": "West", "longitude": "73.029", "woe-name": "Dadra and Nagar Haveli", "latitude": "20.1841", "woe-label": "Dadra and Nagar Haveli, IN, India", "type": "Union Territor" }, "geometry": { "type": "Polygon", "coordinates": [[[677, 3785], [660, 3823], [724, 3866], [758, 3777], [755, 3751], [695, 3750], [677, 3785]]]} }, { "type": "Feature", "id": "IN.MH", "properties": { "hc-group": "admin1", "hc-middle-x": 0.32, "hc-middle-y": 0.45, "hc-key": "in-mh", "hc-a2": "MH", "labelrank": "2", "hasc": "IN.MH", "alt-name": null, "woe-id": "2345750", "subregion": null, "fips": "IN16", "postal-code": "MH", "name": "Maharashtra", "country": "India", "type-en": "State", "region": "West", "longitude": "75.46469999999999", "woe-name": "Maharashtra", "latitude": "19.4723", "woe-label": "Maharashtra, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[677, 3785], [695, 3750], [755, 3751], [758, 3777], [798, 3806], [832, 3799], [834, 3874], [872, 3939], [840, 3981], [873, 4009], [936, 3946], [996, 3961], [1042, 4026], [1029, 4104], [968, 4164], [1008, 4225], [1052, 4227], [1069, 4267], [1192, 4310], [1084, 4330], [1019, 4314], [995, 4355], [1024, 4369], [1020, 4442], [1138, 4483], [1165, 4472], [1241, 4508], [1268, 4477], [1275, 4391], [1346, 4343], [1414, 4342], [1469, 4317], [1498, 4271], [1564, 4247], [1646, 4241], [1772, 4249], [1847, 4235], [1883, 4123], [1975, 4130], [2040, 4166], [2049, 4220], [2084, 4248], [2109, 4311], [2228, 4367], [2266, 4357], [2308, 4385], [2355, 4383], [2388, 4354], [2388, 4293], [2343, 4277], [2363, 4235], [2445, 4229], [2561, 4250], [2685, 4322], [2708, 4276], [2806, 4262], [2882, 4275], [2889, 4300], [2997, 4332], [3006, 4354], [3069, 4345], [3120, 4290], [3210, 4305], [3225, 4284], [3286, 4289], [3352, 4325], [3435, 4258], [3434, 4228], [3533, 4213], [3514, 4170], [3462, 4141], [3444, 4075], [3487, 4043], [3495, 3957], [3467, 3917], [3513, 3919], [3514, 3811], [3441, 3775], [3442, 3741], [3480, 3736], [3481, 3652], [3441, 3641], [3458, 3592], [3483, 3609], [3548, 3524], [3616, 3482], [3593, 3428], [3553, 3396], [3498, 3446], [3435, 3390], [3385, 3284], [3412, 3234], [3385, 3180], [3318, 3174], [3253, 3224], [3269, 3240], [3259, 3313], [3271, 3449], [3258, 3481], [3186, 3528], [3113, 3501], [3069, 3502], [2977, 3546], [2906, 3518], [2832, 3597], [2741, 3617], [2720, 3607], [2665, 3644], [2691, 3583], [2654, 3484], [2606, 3442], [2603, 3399], [2505, 3425], [2478, 3344], [2448, 3315], [2506, 3232], [2431, 3176], [2418, 3129], [2386, 3121], [2351, 3057], [2369, 3027], [2326, 3018], [2277, 3088], [2249, 3039], [2195, 2987], [2122, 2967], [2118, 2901], [2037, 2798], [1977, 2825], [1904, 2765], [1910, 2661], [1860, 2677], [1757, 2654], [1733, 2690], [1700, 2675], [1637, 2714], [1613, 2683], [1646, 2565], [1633, 2520], [1497, 2517], [1462, 2483], [1386, 2517], [1341, 2480], [1345, 2443], [1279, 2425], [1271, 2386], [1225, 2367], [1182, 2403], [1152, 2365], [1110, 2365], [1148, 2304], [1141, 2262], [1186, 2250], [1191, 2194], [1134, 2059], [1079, 2062], [1045, 2014], [985, 2013], [943, 2074], [894, 2052], [803, 2191], [757, 2386], [753, 2588], [717, 2721], [706, 2843], [658, 2984], [659, 3048], [695, 3039], [636, 3098], [639, 3173], [621, 3256], [671, 3294], [639, 3304], [675, 3343], [603, 3341], [615, 3415], [563, 3678], [588, 3767], [612, 3777], [677, 3785]]]} }, { "type": "Feature", "id": "IN.AS", "properties": { "hc-group": "admin1", "hc-middle-x": 0.53, "hc-middle-y": 0.45, "hc-key": "in-as", "hc-a2": "AS", "labelrank": "2", "hasc": "IN.AS", "alt-name": null, "woe-id": "2345741", "subregion": null, "fips": "IN03", "postal-code": "AS", "name": "Assam", "country": "India", "type-en": "State", "region": "Northeast", "longitude": "92.99290000000001", "woe-name": "Assam", "latitude": "26.3302", "woe-label": "Assam, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[6781, 6422], [6863, 6434], [6892, 6472], [6963, 6500], [7038, 6460], [7085, 6458], [7254, 6482], [7282, 6473], [7326, 6513], [7368, 6488], [7432, 6494], [7498, 6529], [7549, 6522], [7563, 6550], [7609, 6549], [7819, 6619], [7895, 6586], [8019, 6610], [8050, 6595], [8131, 6625], [8175, 6664], [8179, 6705], [8308, 6846], [8304, 6894], [8388, 6879], [8672, 7023], [8753, 7033], [8810, 7073], [8897, 7082], [8841, 6967], [8886, 6907], [8893, 6846], [8922, 6868], [8935, 6836], [8902, 6797], [8812, 6771], [8776, 6785], [8737, 6729], [8654, 6679], [8606, 6635], [8565, 6639], [8492, 6549], [8422, 6518], [8382, 6444], [8349, 6463], [8322, 6389], [8268, 6310], [8269, 6216], [8241, 6178], [8197, 6165], [8203, 6209], [8048, 6037], [8084, 6002], [8088, 5990], [8096, 5960], [8098, 5956], [8096, 5948], [8096, 5948], [8096, 5948], [8078, 5920], [8018, 5748], [7994, 5746], [7993, 5654], [7968, 5582], [7901, 5568], [7869, 5603], [7831, 5508], [7773, 5462], [7756, 5503], [7714, 5500], [7685, 5508], [7695, 5561], [7652, 5615], [7679, 5760], [7727, 5737], [7762, 5775], [7724, 5803], [7784, 5865], [7868, 5901], [7859, 5948], [7783, 6002], [7782, 6037], [7702, 6101], [7627, 6071], [7651, 6213], [7615, 6223], [7531, 6198], [7495, 6216], [7460, 6165], [7410, 6185], [7376, 6118], [7336, 6108], [7284, 6062], [7277, 6114], [7219, 6097], [7196, 6139], [7122, 6140], [7041, 6116], [7042, 6144], [6980, 6154], [6897, 6127], [6843, 6061], [6834, 6017], [6858, 5977], [6792, 5916], [6797, 5993], [6775, 6073], [6790, 6110], [6722, 6207], [6780, 6266], [6796, 6348], [6781, 6422]]]} }, { "type": "Feature", "id": "IN.AP", "properties": { "hc-group": "admin1", "hc-middle-x": 0.26, "hc-middle-y": 0.55, "hc-key": "in-ap", "hc-a2": "AP", "labelrank": "2", "hasc": "IN.AP", "alt-name": null, "woe-id": "2345740", "subregion": null, "fips": "IN02", "postal-code": "AP", "name": "Andhra Pradesh", "country": "India", "type-en": "State", "region": "South", "longitude": "79.208", "woe-name": "Andhra Pradesh", "latitude": "16.4854", "woe-label": "Andhra Pradesh, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[3391, 1142], [3338, 1227], [3304, 1184], [3330, 1137], [3384, 1125], [3322, 1123], [3213, 1057], [3201, 1029], [3156, 1055], [3069, 1067], [3061, 1027], [2995, 999], [2961, 959], [2877, 979], [2790, 963], [2761, 939], [2737, 856], [2673, 799], [2600, 850], [2606, 886], [2673, 923], [2731, 1029], [2649, 1091], [2657, 1176], [2586, 1181], [2552, 1218], [2529, 1292], [2486, 1272], [2493, 1324], [2437, 1316], [2437, 1291], [2361, 1239], [2302, 1224], [2280, 1281], [2189, 1307], [2184, 1258], [2117, 1258], [2137, 1306], [2085, 1405], [2123, 1411], [2133, 1369], [2177, 1345], [2245, 1346], [2287, 1300], [2262, 1392], [2314, 1416], [2235, 1473], [2190, 1477], [2153, 1434], [2101, 1445], [2084, 1498], [2111, 1520], [2044, 1567], [2041, 1607], [2073, 1706], [2034, 1733], [2052, 1774], [2151, 1738], [2190, 1787], [2183, 1856], [2153, 1873], [2132, 1932], [2178, 1996], [2160, 2078], [2202, 2111], [2316, 2106], [2331, 2125], [2336, 2273], [2248, 2302], [2315, 2357], [2307, 2407], [2330, 2533], [2296, 2604], [2415, 2737], [2383, 2729], [2313, 2763], [2356, 2808], [2347, 2834], [2392, 2878], [2369, 3027], [2351, 3057], [2386, 3121], [2418, 3129], [2431, 3176], [2506, 3232], [2448, 3315], [2478, 3344], [2505, 3425], [2603, 3399], [2606, 3442], [2654, 3484], [2691, 3583], [2665, 3644], [2720, 3607], [2741, 3617], [2832, 3597], [2906, 3518], [2977, 3546], [3069, 3502], [3113, 3501], [3186, 3528], [3258, 3481], [3271, 3449], [3259, 3313], [3269, 3240], [3253, 3224], [3318, 3174], [3385, 3180], [3415, 3132], [3471, 3136], [3544, 3062], [3573, 2992], [3635, 2935], [3653, 2839], [3676, 2814], [3714, 2838], [3807, 2827], [3876, 2824], [4004, 2905], [4048, 2922], [4123, 2903], [4156, 2924], [4148, 2982], [4168, 3069], [4198, 3111], [4255, 3035], [4251, 3003], [4323, 3049], [4419, 3063], [4435, 3116], [4413, 3161], [4449, 3208], [4477, 3202], [4548, 3253], [4512, 3300], [4568, 3292], [4627, 3356], [4676, 3279], [4699, 3307], [4738, 3232], [4835, 3226], [4893, 3237], [4968, 3326], [5029, 3378], [5061, 3361], [5021, 3288], [4934, 3170], [4832, 3065], [4808, 3025], [4637, 2932], [4582, 2875], [4500, 2752], [4372, 2678], [4306, 2654], [4215, 2596], [4150, 2527], [4139, 2463], [4174, 2460], [4155, 2350], [4037, 2282], [3951, 2247], [3817, 2270], [3756, 2245], [3722, 2113], [3663, 2059], [3621, 2077], [3590, 2007], [3578, 2073], [3494, 2071], [3420, 2030], [3384, 1990], [3318, 1845], [3305, 1731], [3326, 1610], [3359, 1555], [3333, 1434], [3347, 1329], [3375, 1271], [3369, 1215], [3395, 1148], [3391, 1143], [3391, 1142]], [[4132, 2394], [4152, 2410], [4125, 2405], [4105, 2404], [4132, 2394]]]} }, { "type": "Feature", "id": "IN.ML", "properties": { "hc-group": "admin1", "hc-middle-x": 0.31, "hc-middle-y": 0.74, "hc-key": "in-ml", "hc-a2": "ML", "labelrank": "2", "hasc": "IN.ML", "alt-name": null, "woe-id": "2345752", "subregion": null, "fips": "IN18", "postal-code": "ML", "name": "Meghalaya", "country": "India", "type-en": "State", "region": "Northeast", "longitude": "91.3031", "woe-name": "Meghalaya", "latitude": "25.4804", "woe-label": "Meghalaya, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[7724, 5803], [7693, 5828], [7582, 5863], [7485, 5851], [7462, 5833], [7394, 5831], [7308, 5853], [7161, 5820], [7130, 5829], [6997, 5813], [6831, 5862], [6805, 5854], [6792, 5916], [6858, 5977], [6834, 6017], [6843, 6061], [6897, 6127], [6980, 6154], [7042, 6144], [7041, 6116], [7122, 6140], [7196, 6139], [7219, 6097], [7277, 6114], [7284, 6062], [7336, 6108], [7376, 6118], [7410, 6185], [7460, 6165], [7495, 6216], [7531, 6198], [7615, 6223], [7651, 6213], [7627, 6071], [7702, 6101], [7782, 6037], [7783, 6002], [7859, 5948], [7868, 5901], [7784, 5865], [7724, 5803]]]} }, { "type": "Feature", "id": "IN.PB", "properties": { "hc-group": "admin1", "hc-middle-x": 0.68, "hc-middle-y": 0.66, "hc-key": "in-pb", "hc-a2": "PB", "labelrank": "2", "hasc": "IN.PB", "alt-name": null, "woe-id": "2345756", "subregion": null, "fips": "IN23", "postal-code": "PB", "name": "Punjab", "country": "India", "type-en": "State", "region": "North", "longitude": "75.3762", "woe-name": "Punjab", "latitude": "31.0245", "woe-label": "Punjab, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[2168, 7999], [2189, 7973], [2183, 7951], [2144, 7949], [2186, 7908], [2211, 7875], [2211, 7789], [2148, 7801], [2145, 7771], [2081, 7732], [2117, 7719], [2079, 7671], [2033, 7712], [2027, 7687], [1975, 7695], [1955, 7622], [1967, 7584], [1877, 7538], [1818, 7576], [1754, 7551], [1699, 7569], [1623, 7474], [1603, 7525], [1627, 7548], [1571, 7606], [1544, 7600], [1485, 7646], [1424, 7620], [1378, 7635], [1160, 7657], [1190, 7717], [1183, 7744], [1149, 7804], [1226, 7873], [1325, 8017], [1350, 8014], [1441, 8088], [1390, 8126], [1429, 8231], [1391, 8341], [1418, 8392], [1511, 8464], [1584, 8491], [1630, 8485], [1672, 8509], [1695, 8554], [1675, 8583], [1723, 8593], [1745, 8568], [1865, 8653], [1886, 8618], [1809, 8555], [1770, 8484], [1871, 8433], [1898, 8383], [1884, 8366], [1932, 8268], [1961, 8176], [2001, 8164], [2035, 8207], [2055, 8166], [2106, 8130], [2126, 8037], [2168, 7999]]]} }, { "type": "Feature", "id": "IN.RJ", "properties": { "hc-group": "admin1", "hc-middle-x": 0.57, "hc-middle-y": 0.58, "hc-key": "in-rj", "hc-a2": "RJ", "labelrank": "2", "hasc": "IN.RJ", "alt-name": "Greater Rajasthan|Rajputana", "woe-id": "2345757", "subregion": null, "fips": "IN24", "postal-code": "RJ", "name": "Rajasthan", "country": "India", "type-en": "State", "region": "Central", "longitude": "73.8556", "woe-name": "Rajasthan", "latitude": "26.7468", "woe-label": "Rajasthan, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[1378, 7635], [1387, 7600], [1358, 7554], [1402, 7563], [1402, 7444], [1379, 7433], [1404, 7389], [1477, 7414], [1566, 7345], [1653, 7367], [1693, 7257], [1739, 7105], [1803, 7018], [1840, 7007], [1906, 6915], [1856, 6893], [1886, 6879], [1853, 6829], [1874, 6784], [1950, 6782], [1932, 6853], [1942, 6874], [2037, 6907], [2035, 6866], [2070, 6831], [2104, 6875], [2179, 6931], [2208, 6888], [2206, 6781], [2194, 6709], [2251, 6736], [2241, 6762], [2325, 6762], [2347, 6656], [2386, 6593], [2438, 6568], [2455, 6520], [2399, 6475], [2469, 6435], [2384, 6403], [2360, 6357], [2470, 6411], [2567, 6400], [2613, 6417], [2662, 6405], [2646, 6367], [2610, 6357], [2602, 6312], [2539, 6306], [2514, 6271], [2370, 6193], [2332, 6185], [2291, 6142], [2182, 6087], [2116, 6017], [2051, 5991], [2026, 5941], [2050, 5838], [2086, 5805], [2179, 5776], [2275, 5788], [2328, 5827], [2352, 5735], [2331, 5697], [2276, 5703], [2158, 5668], [2165, 5614], [2128, 5591], [2194, 5551], [2226, 5481], [2170, 5457], [2135, 5480], [2137, 5403], [2163, 5356], [2118, 5325], [2077, 5384], [2038, 5348], [2007, 5368], [1925, 5375], [1891, 5396], [1883, 5319], [1840, 5288], [1830, 5250], [1758, 5224], [1730, 5189], [1693, 5200], [1655, 5249], [1674, 5288], [1742, 5270], [1771, 5316], [1751, 5345], [1771, 5390], [1748, 5437], [1800, 5471], [1760, 5575], [1648, 5549], [1560, 5560], [1559, 5627], [1596, 5623], [1607, 5690], [1538, 5685], [1519, 5626], [1482, 5616], [1426, 5645], [1430, 5592], [1479, 5586], [1474, 5550], [1409, 5542], [1381, 5486], [1429, 5450], [1386, 5382], [1435, 5371], [1468, 5290], [1445, 5237], [1453, 5155], [1409, 5083], [1316, 5033], [1303, 4988], [1367, 4958], [1278, 4915], [1223, 4906], [1191, 4954], [1095, 5037], [1068, 5030], [1042, 5069], [976, 5073], [975, 5129], [885, 5212], [909, 5273], [887, 5338], [846, 5304], [789, 5381], [812, 5432], [794, 5500], [765, 5495], [736, 5445], [672, 5455], [606, 5507], [566, 5481], [556, 5514], [428, 5594], [390, 5578], [246, 5595], [139, 5580], [70, 5605], [18, 5730], [9, 5785], [-61, 5904], [-61, 6010], [-159, 6004], [-192, 6016], [-257, 6131], [-229, 6209], [-212, 6346], [-247, 6371], [-332, 6373], [-439, 6440], [-451, 6470], [-430, 6564], [-403, 6618], [-302, 6700], [-238, 6772], [-208, 6846], [-114, 6928], [-74, 6937], [-14, 6891], [9, 6814], [51, 6796], [224, 6851], [311, 6849], [419, 6877], [432, 6934], [468, 6985], [527, 7032], [570, 7146], [616, 7199], [802, 7286], [926, 7486], [979, 7640], [1046, 7672], [1109, 7684], [1183, 7744], [1190, 7717], [1160, 7657], [1378, 7635]]]} }, { "type": "Feature", "id": "IN.UP", "properties": { "hc-group": "admin1", "hc-middle-x": 0.43, "hc-middle-y": 0.53, "hc-key": "in-up", "hc-a2": "UP", "labelrank": "2", "hasc": "IN.UP", "alt-name": "United Provinces", "woe-id": "2345760", "subregion": null, "fips": "IN36", "postal-code": "UP", "name": "Uttar Pradesh", "country": "India", "type-en": "State", "region": "Central", "longitude": "80.9966", "woe-name": "Uttar Pradesh", "latitude": "26.7201", "woe-label": "Uttar Pradesh, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[2646, 6367], [2662, 6405], [2613, 6417], [2567, 6400], [2470, 6411], [2360, 6357], [2384, 6403], [2469, 6435], [2399, 6475], [2455, 6520], [2438, 6568], [2386, 6593], [2347, 6656], [2325, 6762], [2414, 6817], [2393, 6855], [2415, 6937], [2405, 6992], [2346, 7043], [2340, 7123], [2304, 7186], [2285, 7287], [2290, 7396], [2259, 7457], [2303, 7556], [2321, 7624], [2441, 7743], [2447, 7785], [2473, 7789], [2571, 7721], [2532, 7683], [2501, 7622], [2529, 7495], [2572, 7520], [2625, 7492], [2653, 7512], [2642, 7600], [2668, 7608], [2720, 7533], [2754, 7517], [2811, 7443], [2907, 7397], [2838, 7342], [2888, 7314], [2904, 7271], [2954, 7289], [3038, 7217], [3064, 7216], [3089, 7168], [3132, 7180], [3228, 7170], [3268, 7122], [3310, 7156], [3407, 7076], [3457, 7059], [3458, 7096], [3511, 7077], [3635, 6989], [3687, 6979], [3744, 6881], [3775, 6896], [3797, 6863], [3948, 6774], [4007, 6797], [4131, 6708], [4219, 6720], [4256, 6637], [4391, 6620], [4452, 6576], [4471, 6628], [4552, 6628], [4646, 6586], [4679, 6538], [4727, 6420], [4788, 6402], [4791, 6356], [4840, 6312], [4740, 6312], [4677, 6242], [4770, 6216], [4774, 6172], [4705, 6150], [4732, 6096], [4804, 6030], [4844, 6035], [4901, 6005], [4927, 5961], [4887, 5941], [4845, 5966], [4829, 5923], [4799, 5918], [4764, 5952], [4680, 5869], [4496, 5749], [4479, 5724], [4491, 5602], [4545, 5564], [4552, 5472], [4508, 5459], [4528, 5406], [4488, 5318], [4442, 5240], [4355, 5227], [4295, 5259], [4248, 5330], [4269, 5343], [4281, 5411], [4268, 5476], [4281, 5522], [4156, 5540], [4114, 5505], [4098, 5565], [3959, 5617], [3936, 5662], [3852, 5679], [3835, 5726], [3730, 5713], [3709, 5635], [3678, 5610], [3645, 5633], [3563, 5634], [3592, 5719], [3530, 5707], [3516, 5673], [3492, 5703], [3448, 5691], [3469, 5661], [3387, 5651], [3435, 5716], [3385, 5808], [3352, 5808], [3231, 5742], [3235, 5705], [3120, 5714], [3066, 5674], [3047, 5703], [3078, 5746], [3031, 5747], [3022, 5710], [2934, 5714], [2941, 5749], [2887, 5715], [2908, 5820], [2891, 5857], [2855, 5814], [2829, 5841], [2832, 5785], [2782, 5810], [2752, 5752], [2786, 5700], [2796, 5632], [2829, 5573], [2828, 5503], [2873, 5504], [2916, 5436], [2848, 5338], [2740, 5415], [2702, 5372], [2690, 5415], [2647, 5464], [2659, 5527], [2627, 5595], [2687, 5657], [2699, 5721], [2662, 5810], [2701, 5865], [2825, 5893], [2834, 5958], [2859, 5978], [2911, 6083], [2915, 6150], [2950, 6180], [2903, 6319], [2826, 6356], [2774, 6341], [2706, 6378], [2646, 6367]]]} }, { "type": "Feature", "id": "IN.UT", "properties": { "hc-group": "admin1", "hc-middle-x": 0.45, "hc-middle-y": 0.46, "hc-key": "in-ut", "hc-a2": "UT", "labelrank": "2", "hasc": "IN.UT", "alt-name": "Uttarakhand", "woe-id": "20070462", "subregion": null, "fips": "IN39", "postal-code": "UT", "name": "Uttaranchal", "country": "India", "type-en": "State", "region": "Central", "longitude": "79.2841", "woe-name": "Uttaranchal", "latitude": "30.0576", "woe-label": "Uttarakhand, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[2447, 7785], [2440, 7798], [2526, 7839], [2491, 7948], [2520, 8003], [2507, 8019], [2574, 8099], [2608, 8099], [2693, 8145], [2767, 8116], [2868, 8110], [2894, 8076], [2937, 8079], [2898, 8147], [2931, 8155], [2962, 8201], [3008, 8157], [3041, 8076], [3077, 8036], [3138, 8001], [3226, 8011], [3306, 7940], [3364, 7916], [3346, 7850], [3397, 7844], [3535, 7783], [3555, 7755], [3632, 7712], [3579, 7684], [3511, 7615], [3483, 7605], [3450, 7549], [3412, 7529], [3418, 7468], [3359, 7393], [3382, 7351], [3365, 7283], [3330, 7271], [3296, 7194], [3310, 7156], [3268, 7122], [3228, 7170], [3132, 7180], [3089, 7168], [3064, 7216], [3038, 7217], [2954, 7289], [2904, 7271], [2888, 7314], [2838, 7342], [2907, 7397], [2811, 7443], [2754, 7517], [2720, 7533], [2668, 7608], [2642, 7600], [2653, 7512], [2625, 7492], [2572, 7520], [2529, 7495], [2501, 7622], [2532, 7683], [2571, 7721], [2473, 7789], [2447, 7785]]]} }, { "type": "Feature", "id": "IN.JH", "properties": { "hc-group": "admin1", "hc-middle-x": 0.43, "hc-middle-y": 0.59, "hc-key": "in-jh", "hc-a2": "JH", "labelrank": "2", "hasc": "IN.JH", "alt-name": "Vananchal", "woe-id": "20070463", "subregion": null, "fips": "IN38", "postal-code": "JH", "name": "Jharkhand", "country": "India", "type-en": "State", "region": "East", "longitude": "85.05840000000001", "woe-name": "Jharkhand", "latitude": "23.5221", "woe-label": "Jharkhand, IN, India", "type": "State" }, "geometry": { "type": "Polygon", "coordinates": [[[4488, 5318], [4528, 5406], [4508, 5459], [4552, 5472], [4614, 5464], [4694, 5481], [4738, 5513], [4758, 5467], [4833, 5479], [4836, 5448], [4895, 5393], [4936, 5428], [4961, 5421], [5011, 5470], [5071, 5414], [5224, 5481], [5273, 5480], [5321, 5508], [5314, 5539], [5348, 5588], [5409, 5562], [5446, 5581], [5498, 5509], [5547, 5509], [5543, 5463], [5603, 5428], [5621, 5483], [5653, 5516], [5713, 5529], [5763, 5509], [5772, 5541], [5819, 5537], [5837, 5685], [5889, 5727], [5897, 5767], [5938, 5772], [5977, 5830], [6072, 5791], [6073, 5742], [6113, 5666], [6105, 5613], [6117, 5546], [6071, 5546], [6091, 5510], [6065, 5437], [6034, 5417], [6046, 5385], [5984, 5358], [5986, 5327], [5911, 5292], [5900, 5247], [5842, 5230], [5768, 5266], [5743, 5191], [5620, 5152], [5578, 5100], [5535, 5075], [5502, 5124], [5464, 5134], [5455, 5096], [5395, 5083], [5420, 5058], [5398, 4996], [5421, 4958], [5468, 4956], [5515, 4909], [5627, 4903], [5611, 4817], [5681, 4778], [5691, 4749], [5742, 4727], [5732, 4697], [5782, 4637], [5724, 4598], [5649, 4642], [5606, 4638], [5484, 4722], [5448, 4699], [5466, 4658], [5466, 4544], [5446, 4511], [5398, 4506], [5399, 4552], [5358, 4530], [5265, 4561], [5191, 4506], [5150, 4536], [5105, 4536], [5140, 4603], [5130, 4690], [5073, 4657], [4935, 4654], [4896, 4616], [4796, 4636], [4730, 4694], [4809, 4759], [4866, 4833], [4857, 4866], [4779, 4876], [4746, 4933], [4739, 5025], [4714, 5020], [4737, 5093], [4723, 5128], [4698, 5096], [4652, 5104], [4620, 5194], [4569, 5239], [4555, 5290], [4488, 5318]]]}}]
        }
    </script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            var sfCode = '<%= Session["sf_code"] %>';
            var sfType = '<%= Session["sf_type"] %>';
            var DivCode = "";

            if (sfType == "3") {
                DivCode = '<%= Session["division_code"] %>';
            }
            else {
                DivCode = '<%= Session["division_code"] %>';
            }
            if (('<%=Session["sub_division"]%>' != '0' && '<%=Session["sub_division"]%>' != '' && DivCode != "207")) {
                //$('#btnRetailers').parent().css('display', 'none');
            }
           
            if (DivCode == "98") {
                $('#view_subwi').show();
            }
			else if (DivCode == "170"||DivCode == "170,") {
                $('#view_subwi').show();
            }
            //else if (DivCode == "100") {
            //    $('#view_subwi').show();
            //}
			
            var TDate = new Date();
            var TMonth = TDate.getMonth() + 1;
            var TDay = TDate.getDate();
            var TYear = TDate.getFullYear();

            var CDate = TYear + '-' + TMonth + '-' + TDay;

            $("#datepicker").datepicker({ dateFormat: "dd-mm-yy", maxDate: new Date() });
            var viewState = $('#<%=hdndate.ClientID %>').val();
            if (viewState != "") {
                $("#datepicker").val(viewState);
                var st = viewState.split('-');               
                  TMonth = st[1];
             TDay = st[0];
             TYear = st[2];

             CDate = TYear + '-' + TMonth + '-' + TDay;
               

            }
            else {
                $("#datepicker").datepicker("setDate", new Date());
                viewState = TDay + '-' + TMonth + '-' + TYear;;
            }


              $(document).on('change', "#datepicker", function (e) {
                var txt = $('#datepicker').val();
                $('#<%=hdndate.ClientID %>').val(txt);
                __doPostBack(txt, e);
                ShowProgress();
            });

		if(Number('<%=DBase_EReport.Global.ExpenseType%>') > 0){
		    $.ajax({
        	        type: "POST",
                	contentType: "application/json; charset=utf-8",
	                async: true,
	                url: "DashBoard.aspx/GetHyrUpdt",                
        	        dataType: "json",
                	success: function (data) {
                    	
	                },
        	        error: function (result) {
                	    //alert(JSON.stringify(result));
	                }
        	    }); 
		}

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "DashBoard.aspx/GetCatSecondOrderDay",
                data:"{'cdate':'"+CDate+"'}",
                dataType: "json",
                success: function (data) {

                    dountChart('T5Cate', data.d[0].Values, data.d[0].Title, data.d[0].Tot);
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "DashBoard.aspx/GetCatSecondOrderMonth",
                data:"{'cyear':'"+TYear+"','cmonth':'"+TMonth+"'}",
                dataType: "json",
                success: function (data) {
                    dountChart('B5Cate', data.d[0].Values, data.d[0].Title, data.d[0].Tot);
                },
                error: function (result) {
                   // alert(JSON.stringify(result));
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "DashBoard.aspx/GetChanelSecondOrderDay",
                 data:"{'cdate':'"+CDate+"'}",
                dataType: "json",
                success: function (data) {
                    dountChart('T5Chen', data.d[0].Values, data.d[0].Title, data.d[0].Tot);
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "DashBoard.aspx/GetChanelSecondOrderMonth",
                data:"{'cyear':'"+TYear+"','cmonth':'"+TMonth+"'}",
                dataType: "json",
                success: function (data) {
                    dountChart('B5Chen', data.d[0].Values, data.d[0].Title, data.d[0].Tot);
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "DashBoard.aspx/GetpgroupDay",
                data: "{'cdate':'" + CDate + "'}",
                dataType: "json",
                success: function (data) {
                    dountChart('T5pgrop', data.d[0].Values, data.d[0].Title, data.d[0].Tot);
                },
                error: function (result) {
                   
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "DashBoard.aspx/GetpgroupMonth",
                data: "{'cyear':'" + TYear + "','cmonth':'" + TMonth + "'}",
                dataType: "json",
                success: function (data) {
                    dountChart('B5pgrop', data.d[0].Values, data.d[0].Title, data.d[0].Tot);
                },
                error: function (result) {
                   
                }
            });
             var StkDetail = [];
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
					async: true,
                    url: "DashBoard.aspx/GetState",
                    dataType: "json",
                    success: function (data) {
                        var ddlCustomers = $("#ddlstate");
                        //ddlCustomers.empty().append('<option  selected="selected" value="0">State</option>');
                        $.each(data.d, function () {                            
                            ddlCustomers.append($("<option></option>").val(this['StateCode']).html(this['stateName']));
                        });
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });

                

              //giri
              //  var dataSource  = [];
                $(document).on('change', '#ddlstate', function () {
                    var vL = $(this).val();
                    var opVal = $('#ddlstate').val();
                    var opitem = $('#ddlstate option:selected').text();
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
						async: true,
                        url: "DashBoard.aspx/GetDataD",
                        data: "{'State_Code':'" + opVal + "', 'State_name':'" + opitem + "'}",
                        dataType: "json",
                        success: function (data) {  
                                       console.log(data.d);       
                            dts = data.d;
                            // console.log(dts.slice(1, -1));
                            const dataSource = dts;
                            FusionCharts.ready(function () {
                                var myChart = new FusionCharts({
                                    type: "scrollcolumn2d",
                                    renderAt: "chart-containertemp",
                                    width: "720",
                                    height: "350",
                                    dataFormat: "json",
                                    dataSource
                                }).render();
                            });


                        },

                        error: function (jqXHR, exception) {
                            var msg = '';
                            if (jqXHR.status === 0) {
                                msg = 'Not connect.\n Verify Network.';
                            } else if (jqXHR.status == 404) {
                                msg = 'Requested page not found. [404]';
                            } else if (jqXHR.status == 500) {
                                msg = 'Internal Server Error [500].';
                            } else if (exception === 'parsererror') {
                                msg = 'Requested JSON parse failed.';
                            } else if (exception === 'timeout') {
                                msg = 'Time out error.';
                            } else if (exception === 'abort') {
                                msg = 'Ajax request aborted.';
                            } else {
                                msg = 'Uncaught Error.\n' + jqXHR.responseText;
                            }
                           // alert(msg);
                        }
                    });

                });

              
                    var vL = $(this).val();
                    var opVal = $('#ddlstate').val();
                    var opitem = $('#ddlstate option:selected').text();
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
						async: true,
                        url: "DashBoard.aspx/GetDataD",
                        data: "{'State_Code':'" + opVal + "', 'State_name':'" + opitem + "'}",
                        dataType: "json",
                        success: function (data) {
                          console.log(data.d);
                            dts = data.d;
                            // console.log(dts.slice(1, -1));
                            const dataSource = dts;
                            FusionCharts.ready(function () {
                                var myChart = new FusionCharts({
                                    type: "scrollcolumn2d",
                                    renderAt: "chart-containertemp",
                                    width: "720",
                                    height: "350",
                                    dataFormat: "json",
                                    dataSource
                                }).render();
                            });


                        },

                        error: function (jqXHR, exception) {
                            var msg = '';
                            if (jqXHR.status === 0) {
                                msg = 'Not connect.\n Verify Network.';
                            } else if (jqXHR.status == 404) {
                                msg = 'Requested page not found. [404]';
                            } else if (jqXHR.status == 500) {
                                msg = 'Internal Server Error [500].';
                            } else if (exception === 'parsererror') {
                                msg = 'Requested JSON parse failed.';
                            } else if (exception === 'timeout') {
                                msg = 'Time out error.';
                            } else if (exception === 'abort') {
                                msg = 'Ajax request aborted.';
                            } else {
                                msg = 'Uncaught Error.\n' + jqXHR.responseText;
                            }
                            //alert(msg);
                        }
                    });

                    //zone
                     var StkDetail1 = [];
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
					async: true,
                    url: "DashBoard.aspx/GetState",
                    dataType: "json",
                    success: function (data) {
                        var ddlCustomers = $("#ddlstate1");
                        ddlCustomers.empty().append('<option  selected="selected" value="0">All</option>');
                        $.each(data.d, function () {                            
                            ddlCustomers.append($("<option></option>").val(this['StateCode']).html(this['stateName']));
                        });
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });
                     $(document).on('change', '#ddlstate1', function () {
                    var vL = $(this).val();
                    var opVal = $('#ddlstate1').val();
                    var opitem = $('#ddlstate1 option:selected').text();
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
						async: true,
                        url: "DashBoard.aspx/GetDataD1",
                        data: "{'State_Code':'" + opVal + "', 'State_name':'" + opitem + "'}",
                        dataType: "json",
                        success: function (data) {
                            console.log(data.d);
                            dts = data.d;
                            // console.log(dts.slice(1, -1));
                            const dataSource = dts;
                            FusionCharts.ready(function () {
                                var myChart = new FusionCharts({
                                    type: "scrollcolumn2d",
                                    renderAt: "chart-containertemp1",
                                    width: "720",
                                    height: "350",
                                    dataFormat: "json",
                                    dataSource
                                }).render();
                            });


                        },

                        error: function (jqXHR, exception) {
                            
                            var msg = '';
                            if (jqXHR.status === 0) {
                                msg = 'Not connect.\n Verify Network.';
                            } else if (jqXHR.status == 404) {
                                msg = 'Requested page not found. [404]';
                            } else if (jqXHR.status == 500) {
                                msg = 'Internal Server Error [500].';
                            } else if (exception === 'parsererror') {
                                msg = 'Requested JSON parse failed.';
                            } else if (exception === 'timeout') {
                                msg = 'Time out error.';
                            } else if (exception === 'abort') {
                                msg = 'Ajax request aborted.';
                            } else {
                                msg = 'Uncaught Error.\n' + jqXHR.responseText;
                            }
                            //alert(msg);
                        }
                    });

                });

              
                    var vL = $(this).val();
                    var opVal = $('#ddlstate1').val();
                    var opitem = $('#ddlstate1 option:selected').text();
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
						async: true,
                        url: "DashBoard.aspx/GetDataD1",
                        data: "{'State_Code':'" + opVal + "', 'State_name':'" + opitem + "'}",
                        dataType: "json",
                        success: function (data) {
                            console.log(data.d);
                            dts = data.d;
                            // console.log(dts.slice(1, -1));
                            const dataSource = dts;
                            FusionCharts.ready(function () {
                                var myChart = new FusionCharts({
                                    type: "scrollcolumn2d",
                                    renderAt: "chart-containertemp1",
                                    width: "720",
                                    height: "350",
                                    dataFormat: "json",
                                    dataSource
                                }).render();
                            });


                        },

                        error: function (jqXHR, exception) {
                            var msg = '';
                            if (jqXHR.status === 0) {
                                msg = 'Not connect.\n Verify Network.';
                            } else if (jqXHR.status == 404) {
                                msg = 'Requested page not found. [404]';
                            } else if (jqXHR.status == 500) {
                                msg = 'Internal Server Error [500].';
                            } else if (exception === 'parsererror') {
                                msg = 'Requested JSON parse failed.';
                            } else if (exception === 'timeout') {
                                msg = 'Time out error.';
                            } else if (exception === 'abort') {
                                msg = 'Ajax request aborted.';
                            } else {
                                msg = 'Uncaught Error.\n' + jqXHR.responseText;
                            }
                           // alert(msg);
                        }
                    });


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
				async: true,	
                url: "DashBoard.aspx/GetStateSecondOrder",
                data:"{'cdate':'"+CDate+"','cyear':'"+TYear+"','cmonth':'"+TMonth+"'}",
                dataType: "json",
                success: function (data) {
                    DivCode = DivCode.replace(/,\s*$/, "");
                  //  console.log(DivCode);
                    getmap('mapMonth', data.d[0].Values, DivCode);
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
            var priMode = false;
            var perMode = false;
            var PriOrderCatDay = [];
            var priOrderCatMonth = [];
            var priCatTotDay = 0;
            var priCatTotMonth = 0;
            var priCatTitDay = "";
            var priCatTitMonth = "";
            function PrimaryDataLoad() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
					async: true,
                    url: "DashBoard.aspx/GetCatPrimaryOrderDay",
                    data:"{'cdate':'"+CDate+"'}",
                    dataType: "json",
                    success: function (data) {
                        priMode = true;
                        PriOrderCatDay = data.d[0].Values;
                        priCatTotDay = data.d[0].Tot;
                        priCatTitDay = data.d[0].Title;
                        // dountChart('PT5Cate', data.d[0].Values, data.d[0].Title, data.d[0].Tot);
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });


                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
              async: true,
                    url: "DashBoard.aspx/GetCatPrimaryOrderMonth",
                    data:"{'cyear':'"+TYear+"','cmonth':'"+TMonth+"'}",
                    dataType: "json",
                    success: function (data) {
                        priOrderCatMonth = data.d[0].Values;
                        priCatTotMonth = data.d[0].Tot;
                        priCatTitMonth = data.d[0].Title;
                        // dountChart('PB5Cate', data.d[0].Values, data.d[0].Title, data.d[0].Tot);
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });


                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                async: true,
                    url: "DashBoard.aspx/GetStatePrimaryOrder",
                      data:"{'cdate':'"+CDate+"','cyear':'"+TYear+"','cmonth':'"+TMonth+"'}",
                    dataType: "json",
                    success: function (data) {
                        DivCode = DivCode.replace(/,\s*$/, "");
                        //console.log(DivCode);

                        getmap('mapMonthPRI', data.d[0].Values, DivCode);
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });

            }
            var jsonarr = [];
            var ReverseArray = [];
            var prijsonarr = [];
            var priReverseArray = [];


            var TopMonth = [];
            var BotMonth = [];
            var TopDay = [];
            var BotDay = [];

            var priTopMonth = [];
            var priBotMonth = [];
            var priTopDay = [];
            var priBotDay = [];
            function PerformanceLoad() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
               async: true,
                    url: "DashBoard.aspx/GetSecProdctOrderYear",
                    data:"{'cyear':'"+TYear+"'}",
                    dataType: "json",
                    success: function (data) {
                        perMode = true;
                        jsonarr = JSON.parse(data.d[0].Values);
                        var length = jsonarr.length;
                        var j = 0;
                        for (var i = length - 1; i >= 0; i--) {
                            ReverseArray.push(jsonarr[i]);
                            j++;
                        }
                        genChartdata(jsonarr, ReverseArray, 5);
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });


                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
             async: true,
                    url: "DashBoard.aspx/GetSecProdctOrderMonth",
                    data:"{'cyear':'"+TYear+"','cmonth':'"+TMonth+"'}",
                    dataType: "json",
                    success: function (data) {
                        TopMonth = JSON.parse(data.d[0].Values);
                        var length = TopMonth.length;
                        var j = 0;
                        for (var i = length - 1; i >= 0; i--) {
                            BotMonth.push(TopMonth[i]);
                            j++;
                        }
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });


                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
               async: true,
                    url: "DashBoard.aspx/GetSecProdctOrderDay",
                    data:"{'cdate':'"+CDate+"'}",
                    dataType: "json",
                    success: function (data) {
                        TopDay = JSON.parse(data.d[0].Values);
                        var length = TopDay.length;
                        var j = 0;
                        for (var i = length - 1; i >= 0; i--) {
                            BotDay.push(TopDay[i]);
                            j++;
                        }
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });





                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
              async: true,
                    url: "DashBoard.aspx/GetPRIProdctOrderYear",
                    data:"{'cyear':'"+TYear+"'}",
                    dataType: "json",
                    success: function (data) {
                        prijsonarr = JSON.parse(data.d[0].Values);
                        var length = prijsonarr.length;
                        var j = 0;
                        for (var i = length - 1; i >= 0; i--) {
                            priReverseArray.push(prijsonarr[i]);
                            j++;
                        }
                        genChartdata1(prijsonarr, priReverseArray, 5);
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
          async: true,
                    url: "DashBoard.aspx/GetPRIProdctOrderMonth",
                    data:"{'cyear':'"+TYear+"','cmonth':'"+TMonth+"'}",
                    dataType: "json",
                    success: function (data) {
                        priTopMonth = JSON.parse(data.d[0].Values);
                        var length = priTopMonth.length;
                        var j = 0;
                        for (var i = length - 1; i >= 0; i--) {
                            priBotMonth.push(priTopMonth[i]);
                            j++;
                        }
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });


                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
              async: true,
                    url: "DashBoard.aspx/GetPRiProdctOrderDay",
                    data:"{'cdate':'"+CDate+"'}",
                    dataType: "json",
                    success: function (data) {
                        priTopDay = JSON.parse(data.d[0].Values);
                        var length = priTopDay.length;
                        var j = 0;
                        for (var i = length - 1; i >= 0; i--) {
                            priBotDay.push(priTopDay[i]);
                            j++;
                        }
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });


            }

            function genChartdata(obj1, obj2, n) {
                // console.log(n);
                var TopArray = [];
                var BotArray = [];
                var len = obj1.length;
                if (len < n) {
                    n = len;
                }
                for (var i = 0; i < n; i++) {
                    // console.log(TopArray);
                    TopArray.push(obj1[i]);
                    BotArray.push(obj2[i]);
                }

                genBarchart1('proTop', TopArray);
                genBarchart1('proBot', BotArray);
            }

            function genChartdata1(obj1, obj2, n) {


                var TopArray = [];
                var BotArray = [];
                var len = obj1.length;
                if (len < n) {
                    n = len;
                }
                for (var i = 0; i < n; i++) {
                    //  console.log(TopArray);
                    TopArray.push(obj1[i]);
                    BotArray.push(obj2[i]);
                }

                genBarchart1('terrTop', TopArray);
                genBarchart1('terrBot', BotArray);
            }

            //day option

            $(document).on('change', '#opTionType', function () {
                var vL = $(this).val();
                var opVal = $('#opTion').val();
                //  PerformanceLoad();
                if (Number(vL) == 1) {
                    genChartdata(TopDay, BotDay, opVal == 0 ? TopDay.length : opVal);
                      genChartdata1(priTopDay, priBotDay, opVal == 0 ? priTopDay.length : opVal);
                }
                else if (Number(vL) == 2) {
                    genChartdata(TopMonth, BotMonth, opVal == 0 ? TopMonth.length : opVal);
                      genChartdata1(priTopMonth, priBotMonth, opVal == 0 ? priTopMonth.length : opVal);
                }
                else {
                    genChartdata(jsonarr, ReverseArray, opVal == 0 ? jsonarr.length : opVal);
                      genChartdata1(prijsonarr, priReverseArray, opVal == 0 ? prijsonarr.length : opVal);
                }
            });


            //count option
            $(document).on('change', '#opTion', function () {
                var vL = $('#opTionType').val(); ;
                var opVal = $(this).val();
                //  PerformanceLoad();
                if (Number(vL) == 1) {
                    genChartdata(TopDay, BotDay, opVal == 0 ? TopDay.length : opVal);
                     genChartdata1(priTopDay, priBotDay, opVal == 0 ? priTopDay.length : opVal);
                }
                else if (Number(vL) == 2) {
                    genChartdata(TopMonth, BotMonth, opVal == 0 ? TopMonth.length : opVal);
                     genChartdata1(priTopMonth, priBotMonth, opVal == 0 ? priTopMonth.length : opVal);
                }
                else {
                    genChartdata(jsonarr, ReverseArray, opVal == 0 ? jsonarr.length : opVal);
                      genChartdata1(prijsonarr, priReverseArray, opVal == 0 ? prijsonarr.length : opVal)
                }
            });



            $(document).on('click', '#TabSec', function () {
                console.log('Eneter');

            });
            $(document).on('click', '#TabPri', function () {
                if (priMode) {
                    dountChart('PT5Cate', PriOrderCatDay, priCatTitDay, priCatTotDay);
                    dountChart('PB5Cate', priOrderCatMonth, priCatTitMonth, priCatTotMonth);
                }
                else {
                    PrimaryDataLoad();
                    dountChart('PT5Cate', PriOrderCatDay, priCatTitDay, priCatTotDay);
                    dountChart('PB5Cate', priOrderCatMonth, priCatTitMonth, priCatTotMonth);
                }
            });
            $(document).on('click', '#TabPer', function () {
                $('#opTionType').val(3); ;
                $('#opTion').val(5);

                if (perMode) {
                    genChartdata(jsonarr, ReverseArray, 5);
                     genChartdata1(prijsonarr, priReverseArray, 5)
                }
                else {
                    PerformanceLoad();
                    genChartdata(jsonarr, ReverseArray, 5);
                     genChartdata1(prijsonarr, priReverseArray, 5)
                }
            });

            $(document).on('click', '#BtnOrderP', function () {
  if ((DivCode == 78)|| (DivCode == 87) || (DivCode == 89) ){
                    var sURL = "MIS Reports/rpt_stk_wise.aspx?SF_Code=" + sfCode + "&Year=" + TYear + "&Mnth=" + TMonth + "&SF_Name=" + sfCode + "&Sub_Div=" + "<%=sub_divc%>";
                    window.open(sURL, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
                }
                else {
                var sURL = "MIS Reports/rpt_Pri_Order_View.aspx?sf_code=" + sfCode + "&cur_month=" + TMonth + "&cur_year=" + TYear +
               "&Mode=" + "TPMYDayPlan" + "&Sf_Name=" + sfCode + "&Date=" + CDate + "&Type=" + sfType + "&subdiv=" + "<%=sub_divc%>";
                window.open(sURL, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
}
            });


            $(document).on('click', '#BtnOrder', function () {
		  if ((DivCode == 78)|| (DivCode == 87) || (DivCode == 89) ){
                    var sURL = "MIS Reports/rpt_emp_order_valueSTKwise.aspx?SF_Code=" + sfCode + "&Year=" + TYear + "&SF_Name=" + sfCode + "&Date=" + CDate + "&cur_month=" + TMonth + "&Sub_Div=" + "<%=sub_divc%>";
                    window.open(sURL, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
                }
                else {

                var sURL = "MIS Reports/rpt_Total_Order_View.aspx?sf_code=" + sfCode + "&cur_month=" + TMonth + "&cur_year=" + TYear +
               "&Mode=" + "TPMYDayPlan" + "&Sf_Name=" + sfCode + "&Date=" + CDate + "&Type=" + sfType + "&Sub_Div=" + "<%=sub_divc%>";
                window.open(sURL, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
}
            });


            $(document).on('click', '#view_btn', function () {
                var sURL = "MasterFiles/Reports/Rpt_My_Day_Plan_View.aspx?sf_code=" + sfCode + "&cur_month=" + TMonth + "&cur_year=" + TYear +
               "&Mode=" + "TP MY Day Plan" + "&Sf_Name=" + sfCode + "&Date=" + CDate + "&Type=" + sfType + "&Designation_code=" + "" + "&Sub_Div=" + "<%=sub_divc%>";
                window.open(sURL, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
            });

            $(document).on('click', '#view_btn_Outlets', function () {
                var sURL = "MIS Reports/rpt_Visit_OutLets_View.aspx?sf_code=" + sfCode + "&cur_month=" + TMonth + "&cur_year=" + TYear +
               "&Mode=" + "TPMYDayPlan" + "&Sf_Name=" + sfCode + "&Date=" + CDate + "&Type=" + sfType + "&subdiv=" + "<%=sub_divc%>";
                window.open(sURL, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
            });
            $(document).on('click', '#view_fw_dtl', function () {
                var sURL = "MIS Reports/view_countofsf.aspx?SFCode=" + sfCode + "&Dates=" + CDate + "&wtype=F&subdiv=<%=sub_divc%>";
                window.open(sURL, 'newRetailers', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
            });
            $(document).on('click', '#view_lv_dtl', function () {
                var sURL = "MIS Reports/view_countofsf.aspx?SFCode=" + sfCode + "&Dates=" + CDate + "&wtype=L&subdiv=<%=sub_divc%>";
                window.open(sURL, 'newRetailers', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
            });
            $(document).on('click', '#view_ot_dtl', function () {
                var sURL = "MIS Reports/view_countofsf.aspx?SFCode=" + sfCode + "&Dates=" + CDate + "&wtype=N&subdiv=<%=sub_divc%>";
                window.open(sURL, 'newRetailers', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
            });
            $(document).on('click', '#view_nlg_dtl', function () {
                var sURL = "MIS Reports/view_countofsf.aspx?SFCode=" + sfCode + "&Dates=" + CDate + "&wtype=NL&subdiv=<%=sub_divc%>";
                window.open(sURL, 'newRetailers', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
            });

            $(document).on('click', '#view_subwi', function () {
                var sURL = "MIS Reports/view_subdiv_wtype.aspx?SFCode=" + sfCode + "&Dates=" + CDate;
                window.open(sURL, 'newRetailers', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
            });
            $(document).on('click', '#view_New_Outlets', function () {
                var sURL = "MIS Reports/rptNew_Outlet_List.aspx?SFCode=" + sfCode + "&Sf_Name=" + sfCode + "&Dates=" + CDate + "&FYear=&FMonth=&subdiv=<%=sub_divc%>";
                window.open(sURL, 'newRetailers', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
            });

			$(document).on('click', '#btnRetailers', function () {
			    var sURL = "MasterFiles/RetailersDetailsSFwise.aspx?SFCode=" + sfCode + "&SFName=" + sfCode;
                window.open(sURL, 'nRetailers', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
            });

            $(document).on('click', '#btnDistributor', function () {
                var sURL = "MasterFiles/DistributorDashbord.aspx?SFCode=" + sfCode;
                window.open(sURL, 'nDistributor', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
            });


            $(document).on('click', '.newOpt', function () {
                var cId = $(this);
                var cName = cId.text();
                var cMain = $(this).parent('li').parent('ul').siblings('a').find('span').text();
                cId.text(cMain);
                $(this).parent('li').parent('ul').siblings('a').find('span').text(cName);
              //  console.log(cName.trim());
                if (cName.trim() == 'Category') {
                    $('#category').css('display', 'block');
                    $('#Channel').css('display', 'none');
					 $('#pgrop').css('display', 'none');
                    // $('#Brand').css('display', 'none');

                }
                else if (cName.trim() == 'Channel') {
                    $('#category').css('display', 'none');
                    $('#Channel').css('display', 'block');
					 $('#pgrop').css('display', 'none');
                    // $('#Brand').css('display', 'none');
                }
				 else if (cName.trim() == 'Group') {
                    $('#category').css('display', 'none');
                    $('#Channel').css('display', 'none');
                    $('#pgrop').css('display', 'block');
                }
            });
             $(document).on('click', '.pgrp', function () {
                var cId = $(this);
                var cName = cId.text();
                var cMain = $(this).parent('li').parent('ul').siblings('a').find('span').text();
                cId.text(cMain);
                $(this).parent('li').parent('ul').siblings('a').find('span').text(cName);
                //  console.log(cName.trim());
                if (cName.trim() == 'Category') {
                    $('#category').css('display', 'block');
                    $('#Channel').css('display', 'none');
                    $('#pgrop').css('display', 'none');

                }
                else if (cName.trim() == 'Channel') {
                    $('#category').css('display', 'none');
                    $('#Channel').css('display', 'block');
                    $('#pgrop').css('display', 'none');
                }
                else if (cName.trim() == 'Group') {
                    $('#category').css('display', 'none');
                    $('#Channel').css('display', 'none');
                    $('#pgrop').css('display', 'block');
                }
            });

        });
         function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        function popUp() {
            var sf_code = '<%= Session["sf_code"] %>';
            strOpen = "MIS Reports/rpt_Total_Order_View.aspx?sf_code=" + sf_code + "&div_code=" + div_code + "&cur_month=" + FMonth + "&cur_year=" + FYear +
                  "&Mode=" + "TPMYDayPlan" + "&Sf_Name=" + sf_code + "&Date=" + todate
            window.open(strOpen, 'popWindow', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=400,height=600,left = 0,top = 0');
        }
    </script>
    <asp:HiddenField ID="hdndate" runat="server"></asp:HiddenField>
     <div class="row1" style="position:absolute;top: 64px;right: 20px;">

        <input type="text" id="datepicker" class="form-control" style="min-width: 110px !important;width: 110px;display: inline-block;border: none; background-color: transparent;color: #336277; font-family: Verdana; font-weight: bold;
            font-size: 14px;" />
        <label class="caret" for="datepicker" style="position: relative;top: 7px;" ></label>
    </div>
    <div class="row">
        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <div id="btnRetailers" class="info-box bg-pink hover-expand-effect">
                <div class="icon">
                    <i class="fa fa-users"></i>
                </div>
                <div class="content">
                    <div class="number count-to" data-from="0" data-to="125" data-speed="15" data-fresh-interval="20">
                        <asp:Label ID="retailer" runat="server" Text="Label"></asp:Label>
                    </div>
                    <div class="text">
                        RETAILERS</div>
                </div>
            </div>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <div id="btnDistributor" class="info-box bg-purble hover-expand-effect">
                <div class="icon">
                    <i class="fa fa-user"></i>
                </div>
                <div class="content">
                    <div class="number count-to" data-from="0" data-to="125" data-speed="15" data-fresh-interval="20">
                        <asp:Label ID="Dist_cou" runat="server"></asp:Label>
                    </div>
                    <div class="text">
                        DISTRIBUTORS</div>
                </div>
            </div>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <div id="BtnOrder" class="info-box bg-cyan hover-expand-effect">
                <div class="icon">
                    <i class="fa fa-briefcase" aria-hidden="true"></i>
                </div>
                <div class="content">
                    <div class="number count-to" data-from="0" data-to="125" data-speed="15" data-fresh-interval="20">
                        <asp:Label ID="ordercount" runat="server" Text="0"></asp:Label>
                    </div>
                    <div class="text">
                        <%=SecOrderCap %></div>
                </div>
            </div>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <div id="BtnOrderP" class="info-box bg-orange  hover-expand-effect">
                <div class="icon">
                    <i class="fa fa-briefcase" aria-hidden="true"></i>
                </div>
                <div class="content">
                    <div class="number count-to" data-from="0" data-to="125" data-speed="15" data-fresh-interval="20">
                        <asp:Label ID="ordercount_p" runat="server"></asp:Label>
                    </div>
                    <div class="text">
                        PRIMARY ORDERS</div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-8" style="padding: 0px;">
        <div class="col-md-12" style="padding: 0px;display:none">
            <div class="panel panel-default">
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <div id="ChrtPrimSec" class="Chartdown">
                    </div>
                </div>
                <!-- /.panel-body -->
            </div>
        </div>
        <div class="col-md-12" style="padding: 0px;">
            <div id="exTab3" class="container">
                <ul class="nav nav-pills">
                    <li class="active"><a href="#1b" id="TabSec" data-toggle="tab">Secondary</a> </li>
                    <li><a href="#2b" id="TabPri" data-toggle="tab">Primary</a> </li>
                    <li><a href="#3b" id="TabPer" data-toggle="tab">Performance</a> </li>
                    <li><a href="#4b" id="TabPerchart" data-toggle="tab">YTD</a> </li>
                </ul>
            </div>
            <div class="tab-content clearfix">
                <div class="tab-pane active" id="1b">
                    <div class="col-md-12" style="padding: 0px;">
                        <div class="panel panel-default">
                            <%--  <div class="panel-heading">
                                <i class="fa fa-bar-chart-o fa-fw"></i>Category Wise
                            </div>--%>
                            <!-- /.panel-heading -->
                            <div class="panel-body">
                                <div class="row" style="padding: 0px;">
                                    <ul class="nav navbar-nav">
                                        <li class="dropdown user user-menu"><a href="#" id="CATG" class="dropdown-toggle"
                                            data-toggle="dropdown" style="padding: 4px 18px"><i class="fa fa-bar-chart-o fa-fw">
                                            </i><span id="txt1">Category</span><i class="caret"></i> </a>
                                            <ul class="dropdown-menu dropdown-custom dropdown-menu-right">
                                                <li>
                                                    <%-- <a href="#" class="newOpt"><i class="fa fa-user fa-fw pull-right"></i>
                                                        Brand
                                                    </a>--%>
                                                    <a href="#" class="newOpt">Channel </a></li>
													<li>
                                                    <a href="#" class="pgrp">Group</a></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </div>
                                <div class="row" id="category" style="padding: 0px;">
                                    <div class="col-md-6" style="padding: 0px 3px 7px 14px;">
                                        <div id="T5Cate" class="ChartTop">
                                        </div>
                                    </div>
                                    <div class="col-md-6" style="padding: 0px 13px 7px 3px;">
                                        <div id="B5Cate" class="ChartTop">
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="Channel" style="padding: 0px; display: none">
                                    <div class="col-md-6" style="padding: 0px 3px 7px 14px;">
                                        <div id="T5Chen" class="ChartTop">
                                        </div>
                                    </div>
                                    <div class="col-md-6" style="padding: 0px 13px 7px 3px;">
                                        <div id="B5Chen" class="ChartTop">
                                        </div>
                                    </div>
                                </div>
								<div class="row" id="pgrop" style="padding: 0px; display: none">
                                    <div class="col-md-6" style="padding: 0px 3px 7px 14px;">
                                        <div id="T5pgrop" class="ChartTop">
                                        </div>
                                    </div>
                                    <div class="col-md-6" style="padding: 0px 13px 7px 3px;">
                                        <div id="B5pgrop" class="ChartTop">
                                        </div>
                                    </div>
                                </div>
                                <div id="mapchsrt" class="row" style="padding: 0px;">
                                    <div class="col-md-12 ">
                                        <div id="mapMonth">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- /.panel-body -->
                        </div>
                    </div>
                </div>
                <div class="tab-pane" id="2b">
                    <div class="col-md-12" style="padding: 0px;">
                        <div class="panel panel-default">
                            <!-- /.panel-heading -->
                            <div class="panel-body">
                                <!-- /.col-lg-4 (nested) -->
                                <div class="row" style="padding: 0px;">
                                    <div style="padding: 4px 18px">
                                        <i class="fa fa-bar-chart-o fa-fw"></i>Category
                                    </div>
                                    <div class="col-md-6" style="padding: 0px 3px 7px 14px;">
                                        <div id="PT5Cate" class="ChartTop">
                                        </div>
                                    </div>
                                    <div class="col-md-6" style="padding: 0px 13px 7px 3px;">
                                        <div id="PB5Cate" class="ChartTop">
                                        </div>
                                    </div>
                                </div>
                                <div id="Div1" class="row" style="padding: 0px;">
                                    <div class="col-md-12 col-sm-12 col-xs-12 col-lg-12">
                                        <div id="mapMonthPRI">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane" id="3b">
                    <div class="col-md-12" style="padding: 0px;">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        Secondary Order Product Wise</div>
                                    <div class="col-md-6" style="text-align: right">
                                        <select id="opTion">
                                            <option value="5" selected>5</option>
                                            <option value="10">10</option>
                                            <option value="20">20</option>
                                            <option value="0">All</option>
                                        </select>
                                        <select id="opTionType">
                                            <option value="1">Day</option>
                                            <option value="2">Month</option>
                                            <option value="3" selected>Year</option>
                                        </select>
                                    </div>
                                </div>
                                <div id="barchart" class="row" style="padding: 0px;">
                                    <div class="col-md-6">
                                        <div id="proTop" class="ChartTop">
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div id="proBot" class="ChartTop">
                                        </div>
                                    </div>
                                </div>
                                <div id="barchart1" class="row" style="padding: 0px;">
                                    <div class="col-md-12">
                                        Primary Order Product Wise</div>
                                    <div class="col-md-6">
                                        <div id="terrTop" class="ChartTop">
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div id="terrBot" class="ChartTop">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane" id="4b">
                    <div class="col-md-12" style="padding: 0px;">
                        <div class="panel panel-default">
                            <!-- /.panel-heading -->
                            <div class="panel-body">
                                <div style="text-align: center">
                                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <!-- /.panel-body -->
                        </div>
                    </div>
                    <!-- /.chart2 -->
                    <div class="col-md-12" style="padding: 0px;">
                        <div class="panel panel-default">
                            <!-- /.panel-heading -->
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12" style="text-align: right">
                                        <select id="ddlstate" name="ddlstate" class="btn btn-default1 dropdown-toggle" data-toggle="dropdown">
                                            <%--<option class="dropdown-menu scrollable-menu" role="menu"></option>--%>
                                        </select>
                                    </div>
                                </div>
                                <div style="text-align: center">
                                    <%--<asp:Literal ID="Literal2" runat="server"></asp:Literal>--%>
                                    <div id="chart-containertemp">
                                        FusionCharts XT will load here!</div>
                                </div>
                            </div>
                            <!-- /.panel-body -->
                        </div>
                    </div>
                    <!-- /.chart3 -->
                    <div class="col-md-12" style="padding: 0px;">
                        <div class="panel panel-default">
                            <!-- /.panel-heading -->
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12" style="text-align: right">
                                        <select id="ddlstate1" name="ddlstate1" class="btn btn-default1 dropdown-toggle"
                                            data-toggle="dropdown">
                                            <%--<option class="dropdown-menu scrollable-menu" role="menu"></option>--%>
                                        </select>
                                    </div>
                                </div>
                                <div style="text-align: center">
                                    <%--<asp:Literal ID="Literal2" runat="server"></asp:Literal>--%>
                                    <div id="chart-containertemp1">
                                        FusionCharts XT will load here!</div>
                                </div>
                            </div>
                            <!-- /.panel-body -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-xs-4">
                        <i class="fa fa-user fa-5x"></i>
                        <div class="huge">
                            <asp:Label ID="Lbl_Tot_User" runat="server" Text="0"></asp:Label></div>
                        <div>
                            Active Users / Vacant</div>
                    </div>
                    <div class="col-xs-3 text-right">
                        <div class="huge">
						 <a href="#" id="view_fw_dtl" style="color: #fff">
                            <asp:Label ID="Lbl_Reg_User" runat="server" Text="0"></asp:Label></a></div>
                        <div>
                            In-Market!</div>
                        <div class="huge" style="margin-top: 40px;">
						<a href="#" id="view_lv_dtl" style="color: #fff">
                            <asp:Label ID="Lbl_Lea" runat="server" Text="0"></asp:Label></a></div>
                        <div>
                            Leave!</div>
                    </div>
                    <div class="col-xs-5 text-right">
                        <div class="huge">
						 <a href="#" id="view_ot_dtl" style="color: #fff">
                            <asp:Label ID="Lbl_Oth" runat="server" Text="0"></asp:Label></a></div>
                        <div>
                            Other Works!</div>
                        <div class="huge" style="margin-top: 40px; color: #ff9898;">
						<a href="#" id="view_nlg_dtl" style="color: #fff">
                            <asp:Label ID="Lbl_Inact_User" runat="server" Text="0"></asp:Label></a></div>
                        <div>
                            Not Logged In</div>
                    </div>
                </div>
            </div>
            <a href="#" id="view_btn">
                <div class="panel-footer">
                    <span class="pull-left">View Details</span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
                    </i></span>
                    <div class="clearfix">
                    </div>
                </div>
            </a>
			       <a href="#" id="view_subwi" style="display:none;">
                <div class="panel-footer">
                    <span class="pull-left">Division wise Worktype!</span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
                    </i></span>
                     <div class="clearfix">
                    </div>
                </div>
            </a>
        </div>
        <div>
            <div class="panel panel-green">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-3">
                            <i class="fa fa-tasks fa-5x"></i>
                            <div class="huge">
                                <asp:Label ID="Lbl_Outlets" runat="server" Text="0"></asp:Label></div>
                            <div>
                                Visited Outlets</div>
                        </div>
                        <div class="col-xs-3 text-right">
                            <div class="huge">
                                <a href="#" id="view_New_Outlets" style="color: #fff">
                                    <asp:Label ID="Lbl_Sch_Call" runat="server" Text="0"></asp:Label></a></div>
                            <div style="width: 67px;">
                                NewRetailers
                            </div>
                            <div class="huge" style="margin-top: 40px;">
                                <asp:Label ID="Lbl_Prod" runat="server" Text="0"></asp:Label></div>
                            <div>
                                Productive!</div>
                        </div>
                        <div class="col-xs-6 text-right">
                            <div class="huge">
                                <asp:Label ID="Lbl_Prod_Outlet" runat="server" Text="0%"></asp:Label></div>
                            <div>
                                Productivity!</div>
                            <div class="huge" style="margin-top: 40px;">
                                <asp:Label ID="Lbl_Vist_Outlet" runat="server" Text="0"></asp:Label></div>
                            <div>
                                Un-Productive!</div>
                        </div>
                    </div>
                </div>
                <a href="#" id="view_btn_Outlets">
                    <div class="panel-footer">
                        <span class="pull-left">View Details</span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
                        </i></span>
                        <div class="clearfix">
                        </div>
                    </div>
                </a>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <i class="fa fa-bell fa-fw"></i>Notifications Panel
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <div class="list-group">
                        <asp:DataList ID="DataList1" runat="server" RepeatColumns="1" class="list-group-item"
                            OnItemDataBound="Item_Bound">
                            <ItemTemplate>
                                <i class="fa fa-comment fa-fw"></i>
                                <asp:Label ID="lblInput" runat="server" Width="255px" Style="padding-top: 10px; padding-left: 5px;"
                                    Text='<%# Eval("comment") %>'></asp:Label><span class="pull-right text-muted small"><em><asp:Label
                                        ID="daytime" runat="server" Text='<%# Eval("timee") %>' Style="font-style: bold;
                                        font-size: 10px; color: #a8b0b3;"></asp:Label><i class="fa fa-clock-o" style="color: #a6aeb1;"></i></em>
                                    </span>
                                <asp:Label ID="cmttype" runat="server" Text='<%# Eval("Comment_Type") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <!-- /.list-group -->
                    <a href="../../../../MIS Reports/Notification_inpu.aspx" class="btn btn-default btn-block">
                        Set Alerts</a>
                </div>
                <!-- /.panel-body -->
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <i class="fa fa-bar-chart-o fa-fw"></i>In-Time Statistics
                </div>
                <div class="panel-body">
                    <div id="chartContainer" style="height: 200px; width: 100%;">
                    </div>
                    <a href="#" class="btn btn-default btn-block">View Details</a>
                </div>
                <!-- /.panel-body -->
            </div>
        </div>
    </div>
    <div class="loading" align="center">
        Loading. Please wait.<br />
        <br />
        <img src="loader.gif" alt="" />
    </div>
    <script src="https://code.highcharts.com/maps/highmaps.js"></script>
    <script type="text/javascript">
        function getmap(obj, values, chType) {
                    var data = JSON.parse(values);
            var names = 'countries/in/in-all';
            if (Number(chType) == 24 || Number(chType) == 38) {
                names = 'countries/ph/ph-all';
            }
            else if (Number(chType) == 75 || Number(chType) == 85 || Number(chType) == 88) {
                names = 'countries/mm/mm-all';
            }
            else if (Number(chType) == 101) {
                names = 'countries/gh/gh-all';
            }
			else if (Number(chType) == 119) {
                names = 'countries/sl/sl-all';
            }
			else if (Number(chType) == 140) {
                names = 'countries/ng/ng-all';
            }
            Highcharts.mapChart(obj, {
                chart: {
                    map: names,
                    height: 80 + '%'
                   // backgroundColor: '#dedde0'
                },
                title: {
                    text: 'Statewise Summary'                    
                },

                mapNavigation: {
                    enabled: true,
                    buttonOptions: {
                        verticalAlign: 'bottom'
                    }
                },
           
//                colorAxis: {
//            min: 1,           
//            minColor: '#EEEEFF',
//            maxColor: '#000022',
//            stops: [
//                [0, '#EFEFFF'],
//                [0.67, '#4444FF'],
//                [1, '#000022']
//            ]
//        },
// colorAxis: {
// minColor: '#bbf1ce',
//            maxColor: '#00a23b',
//            stops: [
//                [0, '#bbf1ce'],
//                [0.67, '#3a9a5c'],
//                [1, '#00a23b']
//            ]
//            },
colorAxis: {
        min: 0,
        minColor: '#E6E7E8',
        maxColor: '#005645'
    },

//                legend: {
//                    enabled: false
//                },
                plotOptions: {
                    series: {
                        dataLabels: {
                            align: 'left',
                            enabled: true,
                            x: 2,
                            y: -10,
                            style: {
                                textOutline: false
                            }
                        },
                      // colorByPoint: true

                    }

                },
                tooltip: {
                    useHTML: true,
                    headerFormat: '<table>',
                    pointFormat: '<tr><th colspan="2"><h3>{point.name}</h3></th></tr>' +
                                 '<tr><th>Today : </th><td>{point.y}</td></tr>' +
                                 '<tr><th>Month : </th><td>{point.value}</td></tr>',
                    footerFormat: '</table>',
                    followPointer: true
                },
                series: [{
                    animation: {
                        duration: 1000
                    },
                    data: data,
                    joinBy: ['postal-code', 'label'],
                    dataLabels: {
                        enabled: true,
                        allowOverlap: true,
                        shadow: true,
                        style: {
                            fontWeight: 'bold'
                        },
                        format: '<span style="color:#000">{point.name}</span>' + '<br/>' + '<span style="color:#464545">Today:{point.y}</span>' + '<br/>' + '<span style="color:#464545">Month:{point.value}</span>'
                    }
                }]
            });


        }
        function gen1(Obj, value, title, subTit) {
            var chart = AmCharts.makeChart(Obj, {
                "type": "pie",
                "startDuration": 0,
                "labelsEnabled": false,
                "autoMargins": false,
                "marginTop": 2,
                "allLabels": [{
                    "text": subTit,
                    "align": "center",
                    "bold": true,
                    "y": 70
                }, {
                    "text": title,
                    "align": "center",
                    "bold": false,
                    "y": 90
                }],
                "marginBottom": 2,
                "marginLeft": 2,
                "marginRight": 2,
                "pullOutRadius": 0,
                "theme": "light",
                "addClassNames": true,
                "numberFormatter": { precision: 2, decimalSeparator: '.', thousandsSeparator: '' },
                "legend": {
                    "markerType": "circle",
                    "position": "right",
                    "autoMargins": false,
                    "valueWidth": 80,
                    "maxColumns": 1,
                    "enabled": false,
                    "font": "8px Arial,Helvetica,sans-serif"

                },
                "defs": {
                    "filter": [{
                        "id": "shadow",
                        "width": "200%",
                        "height": "200%",
                        "feOffset": {
                            "result": "offOut",
                            "in": "SourceAlpha",
                            "dx": 0,
                            "dy": 0
                        },
                        "feGaussianBlur": {
                            "result": "blurOut",
                            "in": "offOut",
                            "stdDeviation": 5
                        },
                        "feBlend": {
                            "in": "SourceGraphic",
                            "in2": "blurOut",
                            "mode": "normal"
                        }
                    }]
                },
                "innerRadius": "70%",
                "dataProvider": value,
                "balloonText": "[[value]]",
                "valueField": "y",
                "titleField": "label",
                "balloonText": "[[label]]<br><span style='font-size:14px'><b>[[y]]</span>",
                "export": {
                    "enabled": true
                }
            });
            AmCharts.addInitHandler(function (chart) {
                if (chart.dataProvider === undefined || chart.dataProvider.length === 0) {
                    var dp = {};
                    for (var i = 0; i < 3; i++) {
                        dp[chart.titleField] = "";
                        dp[chart.valueField] = 1;
                        chart.dataProvider.push(dp);
                    }
                    chart.labelsEnabled = false;
                    chart.showBalloon = false;
                    chart.addLabel("50%", "50%", "Chart contains no data", "middle", 15);
                    chart.alpha = 0.3;
                }
            }, ["pie"]);
        }
        function gen2(Obj, value, title) {
            var chart = new CanvasJS.Chart(Obj, {
                exportEnabled: false,
                animationEnabled: true,

                legend: {
                    cursor: "pointer",
                    itemclick: explodePie
                },
                data: [{
                    type: "pie",
                    showInLegend: false,
                    toolTipContent: "{label}: <strong>{y}</strong>",
                    indexLabel: "{label} - {y}",
                    exploded: true,
                    dataPoints: value


                }]
            });
            chart.render();
        }

        function explodePie(e) {
            if (typeof (e.dataSeries.dataPoints[e.dataPointIndex].exploded) === "undefined" || !e.dataSeries.dataPoints[e.dataPointIndex].exploded) {
                e.dataSeries.dataPoints[e.dataPointIndex].exploded = false;
            } else {
                e.dataSeries.dataPoints[e.dataPointIndex].exploded = false;
            }
            e.chart.render();

        }

        function genChart1(Obj, arrDta, arr1, title) {
            var chart = AmCharts.makeChart(Obj, {
                "theme": "light",
                "type": "serial",
              
                "allLabels": [{
                    "text": "Primary Vs Secondary",
                    "align": "center",
                    "bold": true,
                    "size": 12,
                    "y": 10
                }],
                "marginTop": 50,

                "startDuration": 0,
                "dataProvider": arrDta,
                "valueAxes": [{
                    "axisAlpha": 0,
                    "position": "left"
                }],
                "graphs": [{
                    "id": "g1",
                    "balloonText": "[[label]]<br><b><span style='font-size:14px;'>[[y]]</span></b>",
                    "bullet": "round",
                    "bulletSize": 8,
                    "lineColor": "#428bca",
                    "lineThickness": 2,
                    "negativeLineColor": "#637bb6",
                    "type": "smoothedLine",
                    "titleField": "Primary",
                    "valueField": "y"
                }, {
                    "id": "g2",
                    "balloonText": "[[label]]<br><b><span style='font-size:14px;'>[[s]]</span></b>",
                    "bullet": "round",
                    "bulletSize": 8,
                    "lineColor": "#5cb85c",
                    "lineThickness": 2,
                    "negativeLineColor": "#d1655d",
                    "type": "smoothedLine",
                    "titleField": "Secondary",
                    "valueField": "s"
                }],
                "dataDateFormat": "mmm-YY",
                "categoryField": "label",
                "categoryAxis": {
                    "gridPosition": "start",
                    "labelRotation": 0
                },
                   "legend": {
        "enabled": false,
         "labelText": " $[[Primary]] [[Secondary]]%"

        
    },
                "export": {
                    "enabled": false
                }
            });
        }

        function dountChart(chartId, values, chName, chTot) {
                var chart = AmCharts.makeChart(chartId, {
                    "type": "pie",
                    "theme": "light",
                    "dataProvider": JSON.parse(values),
                    "titleField": "label",
                    "valueField": "y",
                    "labelRadius": 5,
                    "radius": "45%",
                    "innerRadius": "70%",
                    "labelText": "[[label]]",
                    "labelsEnabled": false,
                    "allLabels": [{
                        "text": chName,
                        "align": "center",
                        "bold": true,
                        "y": 70
                    }, {
                        "text": chTot,
                        "align": "center",
                        "bold": false,
                        "y": 90
                    }]
                });

                AmCharts.addInitHandler(function (chart) {
                    if (chart.dataProvider === undefined || chart.dataProvider.length === 0) {
                        var dp = {};
                        for (var i = 0; i < 3; i++) {
                            dp[chart.titleField] = "";
                            dp[chart.valueField] = 1;
                            chart.dataProvider.push(dp);
                        }
                        chart.labelsEnabled = false;
                        chart.showBalloon = false;
                        chart.addLabel("50%", "50%", " No Data", "middle", 15);
                        chart.alpha = 0.3;
                    }
                }, ["pie"]);
            }
            function genBarchart1(Obj, arrDta) {

                AmCharts.theme = AmCharts.themes.light;
              //  console.log('entre');
                var chart = AmCharts.makeChart(Obj, {
                    "type": "serial",
                    //"theme": "light",
                     "labelsEnabled": false,
                    "marginRight": 70,
                    "dataProvider": arrDta,
                    "gridAboveGraphs": true,
                    "startDuration": 1,
                    "graphs": [{
                        "balloonText": "[[label]]: <b>[[y]]</b>",
                        "fillAlphas": 0.8,
                        "lineAlpha": 0.2,
                        "type": "column",
                        "valueField": "y",
                    }],
                    "chartCursor": {
                        "categoryBalloonEnabled": false,
                        "cursorAlpha": 0,
                        "zoomable": false
                    },
                    "categoryField": "label",
                    "categoryAxis": {
                        "gridPosition": "start",
                        "gridAlpha": 0,
                        "tickPosition": "start",
                        "tickLength": 10,
                       // "inside": true,
                       // "labelRotation": 90
                       "labelsEnabled": false
                    },
                    "export": {
                        "enabled": true
                    }
                });
            }
    </script>
    </form>
</asp:Content>
