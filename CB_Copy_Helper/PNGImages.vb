Imports System.Xml
Imports System.IO

Public Class PNGImages

    Private fold As String

    Property FolderNum() As String
        Get
            Return fold
        End Get
        Set(value As String)
            fold = value
            uiTxtPNGFolder.Text = value
            '' auto search
            uiBtnPNGSearch_Click(New Object, EventArgs.Empty)
        End Set
    End Property

    '' dictionary to hold image data - key is image "id", dictionary holds base64 data, font code, and font title
    Dim images As New Dictionary(Of String, Dictionary(Of String, String))



    Private Sub uiBtnPNGSearch_Click(sender As Object, e As EventArgs) Handles uiBtnPNGSearch.Click
        '' clear list on new search
        uiListBoxPNGFonts.Items.Clear()
        images.Clear()

        Dim folder As String = uiTxtPNGFolder.Text.Trim
        Dim folderFile As String = "g:\PNGFonts\" & folder & ".fnx"
        'Dim folderFile As String = "g:\jdforsythe\dev\CB_Copy_Helper\CB_Copy_Helper\WELCOME.xmll"

        '' if the file doesn't exist, display a message and quit processing
        If (Not File.Exists(folderFile)) Then
            MessageBox.Show("Folder " & folderFile & " does not exist in PNG Font")
            Exit Sub
        End If


        Dim xmlReader As XmlTextReader = New XmlTextReader(folderFile)
        Dim image As String = ""
        Dim id As String = ""
        Dim fontCode As String = ""
        Dim fontTitle As String = ""

        Do While (xmlReader.Read())
            '' we only want things returned that are elements of type "row" with attribute "image"
            If ((xmlReader.NodeType = XmlNodeType.Element) And
                (xmlReader.Name.ToUpper = "ROW") And (xmlReader.HasAttributes)) Then

                '' we only want elements that have an "IMAGE" attribute
                xmlReader.MoveToAttribute("Image")
                '' if it has a value for the image attribute, get the image, id, code, and title values
                If (xmlReader.Value.Length > 0) Then
                    image = xmlReader.Value

                    xmlReader.MoveToAttribute("Id")
                    id = xmlReader.Value

                    xmlReader.MoveToAttribute("Code")
                    fontCode = xmlReader.Value

                    xmlReader.MoveToAttribute("Title")
                    fontTitle = xmlReader.Value

                    '' create a dictionary of the values
                    Dim itemDictionary As New Dictionary(Of String, String)
                    itemDictionary.Add("fontCode", fontCode)
                    itemDictionary.Add("fontTitle", fontTitle)
                    itemDictionary.Add("image", image)

                    '' if the id is already in the images dictionary, remove and replace it
                    If (images.ContainsKey(id)) Then
                        images.Remove(id)
                    End If

                    images.Add(id, itemDictionary)

                End If



            End If
        Loop

        '' now that we have all the newest images, display the list in the list box
        For Each pair As KeyValuePair(Of String, Dictionary(Of String, String)) In images
            uiListBoxPNGFonts.Items.Add(pair.Key)
        Next

    End Sub

    Private Sub decodeImage(ByVal base64 As String)
        ''Dim bt64 As Byte() = System.Convert.FromBase64String(base64)
        Dim ms As New System.IO.MemoryStream(Convert.FromBase64String(base64))
        Dim bmp As New Bitmap(ms)
        ms.Close()

        uiPicBoxEnvelope.SizeMode = PictureBoxSizeMode.Zoom
        uiPicBoxEnvelope.Image = bmp

    End Sub


    Private Sub uiLstBoxPNGFonts_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles uiListBoxPNGFonts.MouseDown
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            '' did we actually click on an item in the box
            Dim selInd As Integer = uiListBoxPNGFonts.IndexFromPoint(e.X, e.Y)
            '' if the returned selindex is not -1, we clicked something
            If (selInd <> -1) Then
                '' show the selected image in the PictureBox
                Dim id = uiListBoxPNGFonts.SelectedItem.ToString
                Dim imageInfo As Dictionary(Of String, String) = images.Item(id)
                Console.WriteLine("id = " & id)
                Console.WriteLine("code = " & imageInfo.Item("fontCode"))
                Console.WriteLine("title = " & imageInfo.Item("fontTitle"))
                Console.WriteLine("image = " & imageInfo.Item("image").Substring(0, 10))
                decodeImage(imageInfo.Item("image"))
            End If

        End If
    End Sub
End Class