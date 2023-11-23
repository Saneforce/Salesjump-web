<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Terms_and_condions.aspx.cs" Inherits="Terms_and_condions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>PDF Viewer</title>
    <script src="https://cdn.jsdelivr.net/npm/pdfjs-dist@2.10.377/build/pdf.min.js"></script>
</head>
<body>

    <div id="pdf-container" style="margin-left: 230px;"></div>

    <script>
        // Function to load and display the PDF
        function loadPDF(pdfUrl) {
            // Fetch the PDF file
            fetch(pdfUrl)
                .then(response => response.arrayBuffer())
                .then(data => {
                    // Load the PDF data
                    pdfjsLib.getDocument({ data }).promise.then(pdf => {
                        // Display each page in a canvas
                        for (let pageNumber = 1; pageNumber <= pdf.numPages; pageNumber++) {
                            pdf.getPage(pageNumber).then(page => {
                                const scale = 1.5;
                                const viewport = page.getViewport({ scale });

                                // Create a canvas for each page
                                const canvas = document.createElement('canvas');
                                const context = canvas.getContext('2d');
                                canvas.height = viewport.height;
                                canvas.width = viewport.width;

                                // Append the canvas to the container
                                document.getElementById('pdf-container').appendChild(canvas);

                                // Render the page on the canvas
                                page.render({
                                    canvasContext: context,
                                    viewport: viewport
                                });
                            });
                        }
                    });
                })
                .catch(error => console.error('Error loading PDF:', error));
        }

        // Call the loadPDF function with the URL of your PDF file
        loadPDF('RAD T&C.pdf');
    </script>

</body>
</html>

</asp:Content>

