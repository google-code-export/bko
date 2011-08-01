﻿Imports System.Threading
Public Class frmSoftware
    Private search_ As Boolean
    Private rCOUNT As Integer
    Private sCOUNT As Integer
    Private m_SortingColumn As ColumnHeader

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        Select Case e.KeyCode

            Case Keys.Enter

                'search_ = True
                Search2(txtSearch.Text)
                FINDTXT_ = txtSearch.Text
                txtSearch.Text = ""

        End Select
    End Sub

    Private Sub Search2(ByVal sFindText As String, Optional ByVal MtchWord As Byte = 0, Optional ByVal MtchCase As Byte = 0)

        lstGroups.Nodes.Clear()
        FINDTXT = sFindText


        search_ = True
        Dim FirstColumn As Boolean

        Dim rs3 As ADODB.Recordset 'Объявляем рекордсет
        Dim sSQL3 As String 'Переменная, где будет размещён SQL запрос
        Dim rs As ADODB.Recordset 'Объявляем рекордсет
        Dim sSQL As String 'Переменная, где будет размещён SQL запрос

        On Error Resume Next

        Dim GID As Integer


        If MtchWord = 1 And MtchCase = 1 Then
            mde = 3
        Else
            If MtchWord = 1 And MtchCase = 0 Then
                mde = 2
            Else
                If MtchCase = 1 And MtchWord = 0 Then
                    mde = 1
                Else
                    If MtchCase = 0 And MtchWord = 0 Then
                        mde = 0
                    End If
                End If
            End If
        End If

        Dim LNGIniFile As New IniFile(sLANGPATH)


        Dim nodeRoot As New TreeNode(ProGramName, 0, 0)
        nodeRoot.Tag = "ROOT" & GENID()

        lstGroups.Nodes.Add(nodeRoot)

        Dim TempNode As New TreeNode(LNGIniFile.GetString("frmSoftware", "MSG1", ""), 0, 0)
        TempNode.Tag = "G1|2"
        nodeRoot.Nodes.Add(TempNode)

        sSQL = "SELECT * FROM SOFT_INSTALL Where soft like '%" & sFindText & "%' or t_lic like '%" & sFindText & "%' or L_key like '%" & sFindText & "%' or Publisher like '%" & sFindText & "%' or TIP like '%" & sFindText & "%'"

        rs = New ADODB.Recordset
        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        With rs
            .MoveFirst()
            Do While Not .EOF
                FINDTXT = ""

                If Not IsDBNull(.Fields("Soft").Value) Then If isThere(.Fields("Soft").Value, sFindText, mde) = True Then GoTo FoundiR
                If Not IsDBNull(.Fields("t_lic").Value) Then If isThere(.Fields("t_lic").Value, sFindText, mde) = True Then GoTo FoundiR
                If Not IsDBNull(.Fields("L_key").Value) Then If isThere(.Fields("L_key").Value, sFindText, mde) = True Then GoTo FoundiR
                If Not IsDBNull(.Fields("Publisher").Value) Then If isThere(.Fields("Publisher").Value, sFindText, mde) = True Then GoTo FoundiR
                If Not IsDBNull(.Fields("TIP").Value) Then If isThere(.Fields("TIP").Value, sFindText, mde) = True Then GoTo FoundiR


FoundiR:
                If Len(FINDTXT) = 0 Then
                Else

                    sSQL3 = "SELECT * FROM kompy where id=" & .Fields("Id_Comp").Value & " order by PSEVDONIM, filial"
                    rs3 = New ADODB.Recordset
                    rs3.Open(sSQL3, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                    With rs3
                        .MoveFirst()
                        Do While Not .EOF
                            If GID <> -1 Then

                                Select Case .Fields("tiptehn").Value

                                    Case "PC"

                                        Dim iC, iA As String
                                        iC = .Fields("TIP_COMPA").Value


                                        If iC = "Ноутбук" Or iC = "notebook" Or iC = "Notebook" Or iC = "NoteBook" Then
                                            iC = "Ноутбук"
                                        End If

                                        If iC = "КПК" Or iC = "Pocket PC" Or iC = "Pocket" Or iC = "Palm" Then
                                            iC = "КПК"
                                        End If

                                        If iC = "Сервер" Or iC = "Server" Or iC = "Сервер для тонких клиентов" Or iC = "Сервер видео наблюдения" Then
                                            iC = "Сервер"
                                        End If

                                        'Pocket PC

                                        Select Case iC

                                            Case "Рабочая станция"
                                                iA = 4


                                            Case "Сервер"

                                                iA = 3

                                            Case "КПК"
                                                iA = 31

                                            Case "Ноутбук"
                                                iA = 5

                                            Case Else
                                                iA = 4

                                        End Select

                                        'Dim TEHNode As New TreeNode(.Fields("NET_NAME").Value, iA, iA)
                                        'TEHNode.Tag = "C|" & .Fields(0).Value
                                        'TempNode.Nodes.Add(TEHNode)

                                        'If .Fields("balans").Value = "1" Or .Fields("balans").Value = "True" Or .Fields("balans").Value = "-1" Then

                                        '    If .Fields("Spisan").Value = "1" Or .Fields("Spisan").Value = "True" Or .Fields("Spisan").Value = "-1" Then
                                        '        TEHNode.NodeFont = New Font(lstGroups.Font, 10)
                                        '    Else
                                        '        TEHNode.NodeFont = New Font(lstGroups.Font, 2)
                                        '    End If

                                        'End If

                                        Filling_TREE_DATA(lstGroups, .Fields("id").Value, TempNode, .Fields("Spisan").Value, .Fields("balans").Value, .Fields("NET_NAME").Value, iA)


                                    Case "Printer"
                                        'Dim TEHNode As New TreeNode(.Fields("NET_NAME").Value, 7, 7)
                                        'TEHNode.Tag = "C|" & .Fields(0).Value
                                        'TempNode.Nodes.Add(TEHNode)

                                        Filling_TREE_DATA(lstGroups, .Fields("id").Value, TempNode, .Fields("Spisan").Value, .Fields("balans").Value, .Fields("NET_NAME").Value, 7)

                                    Case "MFU"

                                        'Dim TEHNode As New TreeNode(.Fields("NET_NAME").Value, 8, 8)
                                        'TEHNode.Tag = "C|" & .Fields(0).Value
                                        'TempNode.Nodes.Add(TEHNode)

                                        Filling_TREE_DATA(lstGroups, .Fields("id").Value, TempNode, .Fields("Spisan").Value, .Fields("balans").Value, .Fields("NET_NAME").Value, 8)

                                    Case "KOpir"
                                        'Dim TEHNode As New TreeNode(.Fields("NET_NAME").Value, 9, 9)
                                        'TEHNode.Tag = "C|" & .Fields(0).Value
                                        'TempNode.Nodes.Add(TEHNode)

                                        Filling_TREE_DATA(lstGroups, .Fields("id").Value, TempNode, .Fields("Spisan").Value, .Fields("balans").Value, .Fields("NET_NAME").Value, 9)

                                    Case "NET"
                                        Dim TEHNode As New TreeNode(.Fields("NET_NAME").Value, 10, 10)
                                        TEHNode.Tag = "C|" & .Fields(0).Value
                                        TempNode.Nodes.Add(TEHNode)

                                        Select Case .Fields("PRINTER_NAME_4").Value

                                            Case "Off"
                                                TEHNode.ForeColor = Color.Red

                                            Case "Defective"

                                                TEHNode.ForeColor = Color.Blue

                                            Case Else

                                                TEHNode.ForeColor = Color.Green

                                        End Select



                                        If .Fields("balans").Value = "1" Or .Fields("balans").Value = "True" Or .Fields("balans").Value = "-1" Then

                                            If .Fields("Spisan").Value = "1" Or .Fields("Spisan").Value = "True" Or .Fields("Spisan").Value = "-1" Then
                                                TEHNode.NodeFont = New Font(lstGroups.Font, 10)
                                            Else
                                                TEHNode.NodeFont = New Font(lstGroups.Font, 2)
                                            End If

                                        End If



                                    Case "PHOTO"
                                        'Dim TEHNode As New TreeNode(.Fields("NET_NAME").Value, 11, 11)
                                        'TEHNode.Tag = "C|" & .Fields(0).Value
                                        'TempNode.Nodes.Add(TEHNode)

                                        Filling_TREE_DATA(lstGroups, .Fields("id").Value, TempNode, .Fields("Spisan").Value, .Fields("balans").Value, .Fields("NET_NAME").Value, 11)

                                    Case "PHONE"
                                        'Dim TEHNode As New TreeNode(.Fields("NET_NAME").Value, 12, 12)
                                        'TEHNode.Tag = "C|" & .Fields(0).Value
                                        'TempNode.Nodes.Add(TEHNode)

                                        Filling_TREE_DATA(lstGroups, .Fields("id").Value, TempNode, .Fields("Spisan").Value, .Fields("balans").Value, .Fields("NET_NAME").Value, 12)

                                    Case "FAX"
                                        'Dim TEHNode As New TreeNode(.Fields("NET_NAME").Value, 13, 13)
                                        'TEHNode.Tag = "C|" & .Fields(0).Value
                                        'TempNode.Nodes.Add(TEHNode)

                                        Filling_TREE_DATA(lstGroups, .Fields("id").Value, TempNode, .Fields("Spisan").Value, .Fields("balans").Value, .Fields("NET_NAME").Value, 13)

                                    Case "SCANER"

                                        'Dim TEHNode As New TreeNode(.Fields("NET_NAME").Value, 14, 14)
                                        'TEHNode.Tag = "C|" & .Fields(0).Value
                                        'TempNode.Nodes.Add(TEHNode)

                                        Filling_TREE_DATA(lstGroups, .Fields("id").Value, TempNode, .Fields("Spisan").Value, .Fields("balans").Value, .Fields("NET_NAME").Value, 14)


                                    Case "ZIP"
                                        'Dim TEHNode As New TreeNode(.Fields("NET_NAME").Value, 15, 15)
                                        'TEHNode.Tag = "C|" & .Fields(0).Value
                                        'TempNode.Nodes.Add(TEHNode)

                                        Filling_TREE_DATA(lstGroups, .Fields("id").Value, TempNode, .Fields("Spisan").Value, .Fields("balans").Value, .Fields("NET_NAME").Value, 15)

                                    Case "OT"

                                        Dim uname As String
                                        Dim rsOT As ADODB.Recordset
                                        rsOT = New ADODB.Recordset
                                        rsOT.Open("SELECT A FROM spr_other where Name ='" & .Fields("tip_compa").Value & "'", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                                        With rsOT
                                            If Not IsDBNull(.Fields("A").Value) Then uname = .Fields("A").Value
                                        End With

                                        rsOT.Close()
                                        rsOT = Nothing

                                        Dim TEHNode As New TreeNode(.Fields("NET_NAME").Value, uname, uname)
                                        TEHNode.Tag = "C|" & .Fields(0).Value
                                        TempNode.Nodes.Add(TEHNode)

                                    Case "MONITOR"
                                        'Dim TEHNode As New TreeNode(.Fields("NET_NAME").Value, 17, 17)
                                        'TEHNode.Tag = "C|" & .Fields(0).Value
                                        'TempNode.Nodes.Add(TEHNode)

                                        Filling_TREE_DATA(lstGroups, .Fields("id").Value, TempNode, .Fields("Spisan").Value, .Fields("balans").Value, .Fields("NET_NAME").Value, 17)

                                        '--------------VIP_Graff Добавление новой перефирии Начало-----------------
                                    Case "USB"
                                        'Dim TEHNode As New TreeNode(.Fields("NET_NAME").Value, 18, 18)
                                        'TEHNode.Tag = "C|" & .Fields(0).Value
                                        'TempNode.Nodes.Add(TEHNode)

                                        Filling_TREE_DATA(lstGroups, .Fields("id").Value, TempNode, .Fields("Spisan").Value, .Fields("balans").Value, .Fields("NET_NAME").Value, 18)

                                    Case "SOUND"
                                        'Dim TEHNode As New TreeNode(.Fields("NET_NAME").Value, 44, 44)
                                        'TEHNode.Tag = "C|" & .Fields(0).Value
                                        'TempNode.Nodes.Add(TEHNode)

                                        Filling_TREE_DATA(lstGroups, .Fields("id").Value, TempNode, .Fields("Spisan").Value, .Fields("balans").Value, .Fields("NET_NAME").Value, 44)

                                    Case "IBP"
                                        'Dim TEHNode As New TreeNode(.Fields("NET_NAME").Value, 41, 41)
                                        'TEHNode.Tag = "C|" & .Fields(0).Value
                                        'TempNode.Nodes.Add(TEHNode)

                                        Filling_TREE_DATA(lstGroups, .Fields("id").Value, TempNode, .Fields("Spisan").Value, .Fields("balans").Value, .Fields("NET_NAME").Value, 41)

                                    Case "FS"
                                        'Dim TEHNode As New TreeNode(.Fields("NET_NAME").Value, 61, 61)
                                        'TEHNode.Tag = "C|" & .Fields(0).Value
                                        'TempNode.Nodes.Add(TEHNode)

                                        Filling_TREE_DATA(lstGroups, .Fields("id").Value, TempNode, .Fields("Spisan").Value, .Fields("balans").Value, .Fields("NET_NAME").Value, 61)

                                    Case "KEYB"
                                        'Dim TEHNode As New TreeNode(.Fields("NET_NAME").Value, 46, 46)
                                        'TEHNode.Tag = "C|" & .Fields(0).Value
                                        'TempNode.Nodes.Add(TEHNode)

                                        Filling_TREE_DATA(lstGroups, .Fields("id").Value, TempNode, .Fields("Spisan").Value, .Fields("balans").Value, .Fields("NET_NAME").Value, 46)

                                    Case "MOUSE"
                                        'Dim TEHNode As New TreeNode(.Fields("NET_NAME").Value, 47, 47)
                                        'TEHNode.Tag = "C|" & .Fields(0).Value
                                        'TempNode.Nodes.Add(TEHNode)

                                        Filling_TREE_DATA(lstGroups, .Fields("id").Value, TempNode, .Fields("Spisan").Value, .Fields("balans").Value, .Fields("NET_NAME").Value, 47)

                                        '--------------VIP_Graff Добавление новой перефирии Конец------------------

                                    Case "CNT"

                                        Dim uname As String
                                        Dim rsOT As ADODB.Recordset
                                        rsOT = New ADODB.Recordset
                                        rsOT.Open("SELECT A FROM spr_other where Name ='" & .Fields("NET_NAME").Value & "'", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                                        With rsOT
                                            If Not IsDBNull(.Fields("A").Value) Then uname = .Fields("A").Value
                                        End With

                                        rsOT.Close()
                                        rsOT = Nothing

                                        'Dim TEHNode As New TreeNode(.Fields("NET_NAME").Value, uname, uname)
                                        'TEHNode.Tag = "C|" & .Fields(0).Value
                                        'TempNode.Nodes.Add(TEHNode)

                                        Filling_TREE_DATA(lstGroups, .Fields("id").Value, TempNode, .Fields("Spisan").Value, .Fields("balans").Value, .Fields("NET_NAME").Value, uname)

                                    Case Else

                                End Select


                            End If

                            'Application.DoEvents()
                            .MoveNext()
                        Loop
                    End With

                    rs3.Close()
                    rs3 = Nothing

                End If
                .MoveNext()
            Loop
        End With
        rs.Close()
        rs = Nothing

        lstGroups.SelectedNode = nodeRoot

        Dim tNode As New TreeNode
        tNode = lstGroups.Nodes(0)
        tNode.Expand()





    End Sub

    Private Sub frmSoftware_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        frmMain.SaveInfTehButton.Enabled = False
        frmMain.ToolStripDropDownButton1.Enabled = False

    End Sub

    Private Sub frmSoftware_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        frmComputers.OneStart = 0

        btnSearch.Image = New System.Drawing.Bitmap(PrPath & "pic\iface\search.png")

        Me.Cursor = Cursors.WaitCursor
        Me.Show()


        Dim objIniFile As New IniFile(PrPath & "base.ini")
        Dim uname As String
        FillComboNET(Me.treebranche, "FILIAL", "SPR_FILIAL", "", False, True)

        Dim LNGIniFile As New IniFile(sLANGPATH)
        treebranche.Items.Add(LNGIniFile.GetString("frmComputers", "MSG53", ""))
        uname = objIniFile.GetString("General", "branche", "")

        If Len(uname) = 0 Then
            treebranche.Text = LNGIniFile.GetString("frmComputers", "MSG53", "")
        Else
            treebranche.Text = uname
        End If

        Me.BeginInvoke(New MethodInvoker(AddressOf frmSoftware_Lang))

        SendFonts(Me)

        If lstGroups.Nodes.Count = 0 Then

            Dim newThread5 As New Thread(AddressOf R_T_LOAD)
            newThread5.Start()

        End If



        FillComboNET(Me.cmbTipLicense, "name", "SPR_LIC", "", False, True)
        FillComboNET(Me.cmbSoftPr, "PROIZV", "SPR_PROIZV", "", False, True)
        FillComboNET(Me.cmbTipPo, "Name", "SPR_TIP_PO", "", False, True)
        FillComboNET(Me.cmbSoftware, "Name", "SPR_PO", "", False, True)

        'cmbSoftware
        DTInstall.Value = Date.Today
        dtGok.Value = Date.Now
        Me.Cursor = Cursors.Default

        frmComputers.OneStart = 1
    End Sub

    Private Sub R_T_LOAD()
        Me.lstGroups.BeginInvoke(New MethodInvoker(AddressOf R_T_LOAD_1))
    End Sub

    Private Sub R_T_LOAD_1()
        Call RefFilTree(Me.lstGroups)
    End Sub

    Private Sub SplitContainer1_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles SplitContainer1.Move

        Call Resizer()

    End Sub

    Private Sub SplitContainer1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles SplitContainer1.Resize

        Call Resizer()



    End Sub

    Private Sub serviceDesc_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize

        Call Resizer()

    End Sub

    Private Sub Resizer()

        If Me.Height < 768 Then Me.Height = 768
        If Me.Width < 1024 Then Me.Width = 1024

        If SplitContainer1.SplitterDistance > 450 Then SplitContainer1.SplitterDistance = 450
        If SplitContainer1.SplitterDistance < 250 Then SplitContainer1.SplitterDistance = 250




    End Sub

    Private Sub SplitContainer1_SplitterMoved(ByVal sender As Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles SplitContainer1.SplitterMoved
        Call Resizer()
    End Sub

    Private Sub lstGroups_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles lstGroups.AfterSelect
        Dim LNGIniFile As New IniFile(sLANGPATH)

        If lstGroups.Nodes.Count <= 0 Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        btnAdd.Text = LNGIniFile.GetString("frmSoftware", "MSG2", "")
        cmbSoftware.Text = ""
        cmbTipLicense.Text = ""
        cmbTipPo.Text = ""
        txtLicKey.Text = ""
        cmbSoftPr.Text = ""
        DTInstall.Value = Date.Today
        dtGok.Value = Date.Today



        Dim d() As String

        d = Split(lstGroups.SelectedNode.Tag, "|")


        Select Case d(0)

            Case "C"

                Dim objIniFile As New IniFile(PrPath & "base.ini")
                objIniFile.WriteString("general", "DK", d(1))
                objIniFile.WriteString("general", "Default", 0)

            Case Else

                Dim objIniFile As New IniFile(PrPath & "base.ini")
                objIniFile.WriteString("general", "DK", 0)
                objIniFile.WriteString("general", "Default", lstGroups.SelectedNode.Tag)

        End Select


        Select Case d(0)

            Case "C"

                Call LOAD_SOFT(d(1), Me.lstSoftware)
                sCOUNT = d(1)

            Case "G"
                lstSoftware.Items.Clear()
                sCOUNT = 0

            Case "O"
                lstSoftware.Items.Clear()
                sCOUNT = 0

            Case "K"
                lstSoftware.Items.Clear()
                sCOUNT = 0

            Case Else
                lstSoftware.Items.Clear()
                sCOUNT = 0


        End Select
        Me.Cursor = Cursors.Default

    End Sub

    Public Sub Soft_Click(Optional ByVal sSid As Integer = 0)

        If lstSoftware.Items.Count = 0 Then Exit Sub


        If sSid <> 0 Then

            rCOUNT = sSid

        Else


            Dim z As Integer

            For z = 0 To lstSoftware.SelectedItems.Count - 1
                rCOUNT = (lstSoftware.SelectedItems(z).Text)
            Next

        End If

        Dim LNGIniFile As New IniFile(sLANGPATH)

        Call SaveActivityToLogDB(LNGIniFile.GetString("frmSoftware", "MSG3", "") & " " & Me.lstGroups.SelectedNode.Text)

        Dim sSQL As String
        Dim rs1 As ADODB.Recordset
        rs1 = New ADODB.Recordset

        sSQL = "SELECT * FROM SOFT_INSTALL WHERE id=" & rCOUNT


        rs1.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        Dim sSw As Date
        Dim sSw2 As Date

        With rs1
            .MoveFirst()
            Do While Not .EOF

                If Not IsDBNull(.Fields("Soft").Value) Then cmbSoftware.Text = .Fields("Soft").Value
                If Not IsDBNull(.Fields("t_lic").Value) Then cmbTipLicense.Text = .Fields("t_lic").Value

                If Not IsDBNull(.Fields("TIP").Value) Then cmbTipPo.Text = .Fields("TIP").Value
                If Not IsDBNull(.Fields("L_key").Value) Then txtLicKey.Text = .Fields("L_key").Value
                If Not IsDBNull(.Fields("Publisher").Value) Then cmbSoftPr.Text = .Fields("Publisher").Value

                'cmbTipPo
                'txtLicKey
                'cmbSoftPr

                If Not IsDBNull(.Fields("d_p").Value) Then sSw = .Fields("d_p").Value
                If Not IsDBNull(.Fields("d_o").Value) Then sSw2 = .Fields("d_o").Value

                If Len(sSw) = 0 Then
                    sSw = Date.Today
                End If

                If Len(sSw2) = 0 Then
                    sSw2 = Date.Today
                End If
                On Error GoTo A

                DTInstall.Value = sSw 'Дата регистрации
                dtGok.Value = sSw2 'Срок исполнения

                .MoveNext()
            Loop
        End With

        btnAdd.Text = LNGIniFile.GetString("frmSoftware", "MSG4", "")

        rs1.Close()
        rs1 = Nothing

        Exit Sub
A:
        DTInstall.Value = Date.Today
        dtGok.Value = Date.Today
    End Sub

    Private Sub lstSoftware_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lstSoftware.ColumnClick
        Dim new_sorting_column As ColumnHeader = _
      lstSoftware.Columns(e.Column)
        Dim sort_order As System.Windows.Forms.SortOrder
        If m_SortingColumn Is Nothing Then
            sort_order = SortOrder.Ascending
        Else
            If new_sorting_column.Equals(m_SortingColumn) Then
                If m_SortingColumn.Text.StartsWith("> ") Then
                    sort_order = SortOrder.Descending
                Else
                    sort_order = SortOrder.Ascending
                End If
            Else
                sort_order = SortOrder.Ascending
            End If

            m_SortingColumn.Text = m_SortingColumn.Text.Substring(2)
        End If

        m_SortingColumn = new_sorting_column
        If sort_order = SortOrder.Ascending Then
            m_SortingColumn.Text = "> " & m_SortingColumn.Text
        Else
            m_SortingColumn.Text = "< " & m_SortingColumn.Text
        End If

        lstSoftware.ListViewItemSorter = New ListViewComparer(e.Column, sort_order)

        lstSoftware.Sort()
    End Sub

    Private Sub lstSoftware_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSoftware.DoubleClick

        Call Soft_Click()


    End Sub

    Private Sub lstSoftware_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstSoftware.SelectedIndexChanged

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim LNGIniFile As New IniFile(sLANGPATH)
        btnAdd.Text = LNGIniFile.GetString("frmSoftware", "MSG2", "")
        cmbSoftware.Text = ""
        cmbTipLicense.Text = ""
        cmbTipPo.Text = ""
        txtLicKey.Text = ""
        cmbSoftPr.Text = ""
        DTInstall.Value = Date.Today
        dtGok.Value = Date.Today



    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.Cursor = Cursors.WaitCursor

        Dim newThread5 As New Thread(AddressOf R_T_LOAD)
        newThread5.Start()

        txtSearch.Text = ""
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If lstSoftware.Items.Count = 0 Then Exit Sub

        Dim z As Integer

        For z = 0 To lstSoftware.SelectedItems.Count - 1
            rCOUNT = (lstSoftware.SelectedItems(z).Text)
        Next

        Dim rs1 As ADODB.Recordset
        rs1 = New ADODB.Recordset

        rs1.Open("Delete FROM SOFT_INSTALL WHERE id=" & rCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        rs1 = Nothing


        Call LOAD_SOFT(sCOUNT, Me.lstSoftware)


    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        On Error GoTo err_

        If sCOUNT = 0 Or Len(sCOUNT) = 0 Then Exit Sub


        If Not (RSExists("PO", "Name", cmbSoftware.Text)) Then
            AddTwoPar(Me.cmbSoftware.Text, Me.cmbSoftPr.Text, "SPR_PO", Me.cmbSoftware)
        End If

        Dim rs2 As ADODB.Recordset
        Dim LNGIniFile As New IniFile(sLANGPATH)

        If btnAdd.Text = LNGIniFile.GetString("frmSoftware", "MSG4", "") Then
            Call SaveActivityToLogDB(LNGIniFile.GetString("frmSoftware", "MSG5", "") & " " & Me.lstGroups.SelectedNode.Text)

            rs2 = New ADODB.Recordset
            rs2.Open("SELECT * FROM SOFT_INSTALL WHERE Id=" & rCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

            With rs2

                .Fields("Soft").Value = cmbSoftware.Text
                .Fields("t_lic").Value = cmbTipLicense.Text
                .Fields("L_key").Value = txtLicKey.Text
                .Fields("d_p").Value = DTInstall.Value
                .Fields("d_o").Value = dtGok.Value
                .Fields("Id_Comp").Value = sCOUNT
                .Fields("Publisher").Value = cmbSoftPr.Text
                .Fields("TIP").Value = cmbTipPo.Text
                .Update()
            End With
            rs2.Close()
            rs2 = Nothing

        Else
            Call SaveActivityToLogDB(LNGIniFile.GetString("frmSoftware", "MSG6", "") & " " & Me.lstGroups.SelectedNode.Text)
            rs2 = New ADODB.Recordset
            rs2.Open("SELECT * FROM SOFT_INSTALL", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

            With rs2
                .AddNew()
                .Fields("Soft").Value = cmbSoftware.Text
                .Fields("t_lic").Value = cmbTipLicense.Text
                .Fields("L_key").Value = txtLicKey.Text
                .Fields("d_p").Value = DTInstall.Value
                .Fields("d_o").Value = dtGok.Value
                .Fields("Id_Comp").Value = sCOUNT
                .Fields("Publisher").Value = cmbSoftPr.Text
                .Fields("TIP").Value = cmbTipPo.Text
                .Update()
            End With
            rs2.Close()
            rs2 = Nothing


        End If

        btnAdd.Text = LNGIniFile.GetString("frmSoftware", "MSG2", "")
        cmbSoftware.Text = ""
        cmbTipLicense.Text = ""
        cmbTipPo.Text = ""
        txtLicKey.Text = ""
        cmbSoftPr.Text = ""
        DTInstall.Value = Date.Today
        dtGok.Value = Date.Today

        Call LOAD_SOFT(sCOUNT, Me.lstSoftware)
        Exit Sub
err_:

        MsgBox(Err.Description)
    End Sub

    Private Sub cmbSoftware_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSoftware.SelectedIndexChanged
        On Error Resume Next
        Dim rs As ADODB.Recordset
        Dim sSQL As String
        Dim uNI As String
        rs = New ADODB.Recordset
        cmbTipPo.Text = ""
        cmbSoftPr.Text = ""


        sSQL = "SELECT * FROM SPR_PO WHERE Name = '" & cmbSoftware.Text & "'"

        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        With rs

            If Not IsDBNull(.Fields("proizv").Value) Then uNI = .Fields("proizv").Value
            If Not IsDBNull(.Fields("A").Value) Then cmbTipPo.Text = .Fields("A").Value

            Dim PROYZV As ADODB.Recordset
            PROYZV = New ADODB.Recordset
            PROYZV.Open("SELECT * FROM SPR_PROIZV WHERE iD=" & uNI, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

            With PROYZV
                cmbSoftPr.Text = .Fields("proizv").Value
            End With
            PROYZV.Close()
            PROYZV = Nothing

        End With

        rs.Close()
        rs = Nothing
    End Sub

    Private Sub lstGroups_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles lstGroups.NodeMouseClick

        '#############################################
        'Выделение по правому клику мышкой
        '#############################################

        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.lstGroups.SelectedNode = e.Node
        End If

    End Sub


    Private Sub treebranche_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles treebranche.SelectedIndexChanged



        Dim objIniFile As New IniFile(PrPath & "base.ini")
        objIniFile.WriteString("General", "branche", treebranche.Text)

        If frmComputers.OneStart = 0 Then Exit Sub

        Dim newThread5 As New Thread(AddressOf R_T_LOAD)
        newThread5.Start()


    End Sub
End Class