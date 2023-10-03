<%@ Page Title="" Language="C#" MasterPageFile="~/Billing.master" AutoEventWireup="true" CodeFile="CompanyCreationRequest.aspx.cs" Inherits="CompanyCreationRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
        .sub-header {
            font-family: Verdana;
            font-size: x-large;
            font-weight: bold;
        }
        .card {
            position: relative;
            display: flex;
            flex-direction: column;
            min-width: 0;
            word-wrap: break-word;
            background-color: #fff;
            background-clip: border-box;
            border: 1px solid #eee;
            /* border-radius: 0.25rem; */
        }

        .card {
            border: 0;
            margin-bottom: 30px;
            margin-top: 30px;
            border-radius: 6px;
            color: #333;
            background: #fff;
            width: 100%;
            /* box-shadow: 0 2px 2px 0 rgb(0 0 0 / 14%), 0 3px 1px -2px rgb(0 0 0 / 20%), 0 1px 5px 0 rgb(0 0 0 / 12%); */
        }
        .card {
            box-shadow: 0 1px 4px 0 rgb(0 0 0 / 14%);
            padding: 8px 16px;    
            margin-top: 0px;
        }
        .row {
            margin-top:10px;
        }
    </style>
        <div class="card" style="display:none">
            <div class="card-body table-responsive">
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />
                    <label style="white-space: nowrap; margin-left: 57px;">Filter By&nbsp;&nbsp;</label><select id="txtfilter" name="ddfilter" style="width: 250px;"></select>
                    <label style="float: right">Show
                        <select class="data-table-basic_length" aria-controls="data-table-basic">
                            <option value="10">10</option>
                            <option value="25">25</option>
                            <option value="50">50</option>
                            <option value="100">100</option>
                        </select>
                        entries</label>
                </div>
                <table class="table table-hover" id="OrderList" style="font-size: 12px;">
                    <thead class="text-warning">
                        <tr style="white-space: nowrap;">
                            <th style="text-align:left">SlNo</th>
                            <th style="text-align:left">Customer ID</th>
                            <th style="text-align:left">Customer Name</th>
                            <th style="text-align:left">Code</th>
                            <th style="text-align:left">URL</th>
                            <th style="text-align:left">Mobile.No</th>
                            <th style="text-align:left">State</th>
                            <th style="text-align:left">User Count</th>
                            <th style="text-align:left">Cost</th>
                            <th style="text-align:left">Edit</th>
                            <th style="text-align:left">Status</th>                            
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <div class="row">
                    <div class="col-sm-5">
                        <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 1 to 10 of 57 entries</div>
                    </div>
                    <div class="col-sm-7">
                        <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                            <ul class="pagination">
                                <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                                <li class="paginate_button active"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">1</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="2" tabindex="0">2</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="3" tabindex="0">3</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="4" tabindex="0">4</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="5" tabindex="0">5</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="6" tabindex="0">6</a></li>
                                <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="row">
                <div class="col-lg-6 sub-header">Site Creation Request</div>
            </div>
            <div class="row">
                <div class="col-lg-6">
                    <div class="row">
                        <div class="col-lg-3">Company Name</div><div class="col-lg-9"><input type="text" id="tSearchOrd" style="width: 250px;" /></div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3">Company Name</div><div class="col-lg-9"><input type="text" id="tSearchOrd" style="width: 250px;" /></div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3">Company Name</div><div class="col-lg-9"><input type="text" id="tSearchOrd" style="width: 250px;" /></div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3">Company Name</div><div class="col-lg-9"><input type="text" id="tSearchOrd" style="width: 250px;" /></div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3">Company Name</div><div class="col-lg-9"><input type="text" id="tSearchOrd" style="width: 250px;" /></div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6" >  <button type="button" id="btnsubmit" class="btn btn-primary">Submit</button></div>
            </div>
        </div>
    <script type="text/javascript" src="js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="js/plugins/datatables/dataTables.bootstrap.js"></script>
</asp:Content>

