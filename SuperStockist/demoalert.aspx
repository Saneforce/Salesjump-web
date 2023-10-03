<%@ Page Title="" Language="C#" MasterPageFile="~/Master_SS.master" AutoEventWireup="true" CodeFile="demoalert.aspx.cs" Inherits="SuperStockist_demoalert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .toast {
            position: fixed;
            top: 20px;
            right: 20px;
            z-index: 9999;
            max-width: 350px;
            opacity: 0;
            transition: opacity 0.3s ease-in-out;
        }

            .toast.show {
                opacity: 1;
            }

        .toast-header {
            display: flex;
            align-items: center;
            justify-content: space-between;
            background-color: #f8d7da;
            color: #721c24;
            padding: 0.5rem 1rem;
            border-radius: 0.25rem 0.25rem 0 0;
        }

        .toast-body {
            background-color: #f8d7da;
            color: #721c24;
            padding: 1rem;
            border-radius: 0 0 0.25rem 0.25rem;
        }
    </style>
    <script>
$(document).ready(function() {
  $('.toast').toast('show');
});

    </script>
    <div class="toast">
        <div class="toast-header">
            <strong class="mr-auto">Notification</strong>
            <button type="button" class="ml-2 mb-1 close" data-dismiss="toast">&times;</button>
        </div>
        <div class="toast-body">
            This is a notification message.
        </div>
    </div>

</asp:Content>

