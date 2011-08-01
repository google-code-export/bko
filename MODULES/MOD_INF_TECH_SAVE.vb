﻿Module MOD_INF_TECH_SAVE
    Private Sav As Boolean
    Private DV As Boolean
    Public MRZD As Boolean = False

    Private Sub SAVE_GARANT(ByVal sID As String, ByVal dPost As ComboBox, ByVal dtp As DateTimePicker, ByVal dto As DateTimePicker)

        Dim sSQL As String

        Dim rs As ADODB.Recordset
        rs = New ADODB.Recordset
        rs.Open("Delete FROM Garantia_sis WHERE Id_Comp =" & sID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        rs = Nothing

        sSQL = "SELECT * FROM Garantia_sis"


        rs = New ADODB.Recordset
        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        If Not (RSExists("Postav", "name", dPost.Text)) Then
            AddOnePar(dPost.Text, "name", "SPR_Postav", dPost)
        End If

        With rs
            .AddNew()
            .Fields("Id_Comp").Value = sID
            .Fields("Postav").Value = dPost.Text
            .Fields("day").Value = dtp.Value.Day
            .Fields("month").Value = dtp.Value.Month
            .Fields("Year").Value = dtp.Value.Year
            .Fields("day_o").Value = dto.Value.Day
            .Fields("month_o").Value = dto.Value.Month
            .Fields("Year_o").Value = dto.Value.Year
            .Update()
        End With
        rs.Close()
        rs = Nothing


    End Sub

    Public Sub SAVE_MON(Optional ByVal sSID As String = "")
        On Error GoTo Err_

        If Len(frmComputers.cmbOTH.Text) = 0 Or Len(frmComputers.cmbOTHFil.Text) = 0 Then

            MsgBox("Не заполнены обязательные поля", MsgBoxStyle.Information, ProGramName)

            If Len(frmComputers.cmbOTH.Text) = 0 Then frmComputers.cmbOTH.BackColor = Color.Red
            If Len(frmComputers.cmbOTHFil.Text) = 0 Then frmComputers.cmbOTHFil.BackColor = Color.Red

            frmComputers.sSTAB3.SelectedTab = frmComputers.sSTAB3.TabPages("TabPage11")
            Exit Sub
        End If

        If frmComputers.EDT = True Then
            Call DVIG_TEHN(frmComputers.cmbOTHFil.Text, frmComputers.cmbOTHDepart.Text, frmComputers.cmbOTHOffice.Text, frmComputers.cmbOTH.Text)

            If DV = True Then

            Else
                If Sav = False Then
                    MsgBox("Отмена перемещения", MsgBoxStyle.Exclamation, ProGramName)
                    Exit Sub
                End If

            End If

        End If

        PRESAVE_TREE(frmComputers.cmbOTHFil, frmComputers.cmbOTHDepart, frmComputers.cmbOTHOffice)

        Dim sSQL As String

        If Len(sSID) = 0 Then

            sSQL = "SELECT * FROM kompy"

        Else

            sSQL = "SELECT * FROM kompy where id=" & sSID

        End If

        If Not (RSExists("otv", "name", Trim(frmComputers.cmbOTHotv.Text))) Then
            AddOnePar(frmComputers.cmbOTHotv.Text, "NAME", "SPR_OTV", frmComputers.cmbOTHotv)
        End If

        If Not (RSExists("MONITOR", "name", frmComputers.cmbOTH.Text)) Then
            AddTreePar(frmComputers.cmbOTH.Text, frmComputers.txtMonDum.Text, frmComputers.PROiZV39.Text, "SPR_MONITOR", frmComputers.cmbOTH)
        End If

        Dim rs As ADODB.Recordset
        Dim unaPCL As String
        If Len(frmComputers.cmbOTHPCL.Text) <> 0 Then

            On Error GoTo sAR

            rs = New ADODB.Recordset
            rs.Open("Select id From kompy where filial='" & frmComputers.cmbOTHFil.Text & "' and mesto='" & frmComputers.cmbOTHDepart.Text & "' and kabn='" & frmComputers.cmbOTHOffice.Text & "' and NET_NAME='" & frmComputers.cmbOTHPCL.Text & "'", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)


            With rs

                unaPCL = .Fields("id").Value

            End With
            rs.Close()
            rs = Nothing
        End If

sAR:

        If Len(unaPCL) = 0 Or unaPCL = Nothing Then unaPCL = 0
        'Dim rs As ADODB.Recordset
        rs = New ADODB.Recordset

        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)


        With rs
            If Len(sSID) = 0 Then
                .AddNew()
            End If


            .Fields("MONITOR_NAME").Value = frmComputers.cmbOTH.Text
            .Fields("MONITOR_DUM").Value = frmComputers.txtMonDum.Text
            .Fields("MONITOR_SN").Value = frmComputers.txtOTHSN.Text
            .Fields("MONITOR_PROIZV").Value = frmComputers.PROiZV39.Text

            .Fields("port_1").Value = frmComputers.txtOTHmemo.Text

            .Fields("OTvetstvennyj").Value = frmComputers.cmbOTHotv.Text
            .Fields("INV_NO_MONITOR").Value = frmComputers.txtOTHinnumber.Text

            .Fields("FILIAL").Value = frmComputers.cmbOTHFil.Text
            .Fields("MESTO").Value = frmComputers.cmbOTHDepart.Text
            .Fields("kabn").Value = frmComputers.cmbOTHOffice.Text

            .Fields("TELEPHONE").Value = frmComputers.txtOTHphone.Text
            .Fields("TIPtehn").Value = TipTehn

            .Fields("NET_NAME").Value = frmComputers.cmbOTH.Text
            .Fields("PSEVDONIM").Value = frmComputers.cmbOTH.Text
            .Fields("PCL").Value = unaPCL


            If Len(frmComputers.txtOTHSfN.Text) = 0 Then frmComputers.txtOTHSfN.Text = 0
            If Len(frmComputers.txtOTHcash.Text) = 0 Then frmComputers.txtOTHcash.Text = 0
            If Len(frmComputers.txtOTHSumm.Text) = 0 Then frmComputers.txtOTHSumm.Text = 0



            .Fields("SFAktNo").Value = frmComputers.txtOTHSfN.Text
            .Fields("CenaRub").Value = frmComputers.txtOTHcash.Text
            .Fields("StoimRub").Value = frmComputers.txtOTHSumm.Text
            .Fields("Zaiavk").Value = frmComputers.txtOTHZay.Text

            .Fields("DataVVoda").Value = frmComputers.dtOTHdataVvoda.Value
            .Fields("dataSF").Value = frmComputers.dtOTHSFdate.Value

            .Fields("Spisan").Value = frmComputers.chkOTHspis.Checked
            .Fields("Balans").Value = frmComputers.chkOTHNNb.Checked


            .Update()
        End With
        rs.Close()
        rs = Nothing



        If frmComputers.EDT = False Then

            Dim rsBK As ADODB.Recordset
            rsBK = New ADODB.Recordset
            rsBK.Open("SELECT id FROM kompy WHERE NET_NAME='" & frmComputers.cmbOTH.Text & "' and MESTO='" & frmComputers.cmbOTHDepart.Text & "' and FILIAL='" & frmComputers.cmbOTHFil.Text & "'  and kabn='" & frmComputers.cmbOTHOffice.Text & "'", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

            With rsBK

                frmComputers.sCOUNT = .Fields("ID").Value

            End With
            rsBK.Close()
            rsBK = Nothing

            Dim objIniFile As New IniFile(PrPath & "base.ini")
            objIniFile.WriteString("general", "DK", frmComputers.sCOUNT)
            objIniFile.WriteString("general", "Default", 0)

        Else

        End If

        If Len(sSID) = 0 Then
            sSID = frmComputers.sCOUNT
        End If

        SAVE_GARANT(sSID, frmComputers.cmbOTHPostav, frmComputers.dtGOTHPr, frmComputers.dtGOTHok)

        frmComputers.cmbOTH.BackColor = frmComputers.cmbOTH.BackColor
        frmComputers.cmbOTHFil.BackColor = frmComputers.cmbOTHFil.BackColor

        If frmComputers.pDRAG = True Then

        Else
            RefFilTree(frmComputers.lstGroups)
        End If


        Exit Sub
Err_:

    End Sub

    Public Sub Save_OT(Optional ByVal sSID As String = "")
        On Error GoTo Err_


        If Len(frmComputers.cmbOTH.Text) = 0 Or Len(frmComputers.cmbOTHFil.Text) = 0 Then

            MsgBox("Не заполнены обязательные поля", MsgBoxStyle.Information, ProGramName)

            If Len(frmComputers.cmbOTH.Text) = 0 Then frmComputers.cmbOTH.BackColor = Color.Red
            If Len(frmComputers.cmbOTHFil.Text) = 0 Then frmComputers.cmbOTHFil.BackColor = Color.Red

            frmComputers.sSTAB3.SelectedTab = frmComputers.sSTAB3.TabPages("TabPage11")
            Exit Sub
        End If

        If frmComputers.EDT = True Then
            Call DVIG_TEHN(frmComputers.cmbOTHFil.Text, frmComputers.cmbOTHDepart.Text, frmComputers.cmbOTHOffice.Text, frmComputers.cmbOTH.Text)

            If DV = True Then

            Else
                If Sav = False Then
                    MsgBox("Отмена перемещения", MsgBoxStyle.Exclamation, ProGramName)
                    Exit Sub
                End If

            End If

        End If

        PRESAVE_TREE(frmComputers.cmbOTHFil, frmComputers.cmbOTHDepart, frmComputers.cmbOTHOffice)

        Dim sSQL As String

        If Len(sSID) = 0 Then

            sSQL = "SELECT * FROM kompy"

        Else

            sSQL = "SELECT * FROM kompy where id=" & sSID

        End If

        Select Case TipTehn


            Case "OT"

                If Not RSExists("OTHER", "name", Trim(frmComputers.cmbOTHConnect.Text)) Then
                    AddOnePar(frmComputers.cmbOTHConnect.Text, "NAME", "spr_other", frmComputers.cmbOTHConnect)
                End If

            Case "PHOTO"

                If Not RSExists("PHOTO", "name", Trim(frmComputers.cmbOTH.Text)) Then
                    AddTwoPar(frmComputers.cmbOTH.Text, frmComputers.PROiZV39.Text, "spr_photo", frmComputers.cmbOTH)
                End If

            Case "FAX"

                If Not RSExists("FAX", "name", Trim(frmComputers.cmbOTH.Text)) Then
                    AddTwoPar(frmComputers.cmbOTH.Text, frmComputers.PROiZV39.Text, "spr_fax", frmComputers.cmbOTH)
                End If


            Case "PHONE"

                If Not RSExists("PHONE", "name", Trim(frmComputers.cmbOTH.Text)) Then
                    AddTwoPar(frmComputers.cmbOTH.Text, frmComputers.PROiZV39.Text, "spr_phone", frmComputers.cmbOTH)
                End If

            Case "ZIP"
                'spr_zip
                If Not RSExists("spr_zip", "name", Trim(frmComputers.cmbOTH.Text)) Then
                    AddTwoPar(frmComputers.cmbOTH.Text, frmComputers.PROiZV39.Text, "spr_zip", frmComputers.cmbOTH)
                End If

            Case "SCANER"

                If Not RSExists("SCANER", "name", Trim(frmComputers.cmbOTH.Text)) Then
                    AddTwoPar(frmComputers.cmbOTH.Text, frmComputers.PROiZV39.Text, "SPR_SCANER", frmComputers.cmbOTH)
                End If

            Case "USB"
                If Not RSExists("USB", "name", Trim(frmComputers.cmbOTH.Text)) Then
                    AddTwoPar(frmComputers.cmbOTH.Text, frmComputers.PROiZV39.Text, "SPR_USB", frmComputers.cmbOTH)
                End If

            Case "SOUND"
                If Not RSExists("ASISTEM", "name", Trim(frmComputers.cmbOTH.Text)) Then
                    AddTwoPar(frmComputers.cmbOTH.Text, frmComputers.PROiZV39.Text, "SPR_ASISTEM", frmComputers.cmbOTH)
                End If

            Case "IBP"

                If Not RSExists("IBP", "name", Trim(frmComputers.cmbOTH.Text)) Then
                    AddTwoPar(frmComputers.cmbOTH.Text, frmComputers.PROiZV39.Text, "SPR_IBP", frmComputers.cmbOTH)
                End If

            Case "FS"
                If Not RSExists("SPR_FS", "name", Trim(frmComputers.cmbOTH.Text)) Then
                    AddTwoPar(frmComputers.cmbOTH.Text, frmComputers.PROiZV39.Text, "SPR_FS", frmComputers.cmbOTH)
                End If

            Case "KEYB"

                If Not RSExists("KEYBOARD", "name", Trim(frmComputers.cmbOTH.Text)) Then
                    AddTwoPar(frmComputers.cmbOTH.Text, frmComputers.PROiZV39.Text, "SPR_KEYBOARD", frmComputers.cmbOTH)
                End If

            Case "MOUSE"
                If Not RSExists("MOUSE", "name", Trim(frmComputers.cmbOTH.Text)) Then
                    AddTwoPar(frmComputers.cmbOTH.Text, frmComputers.PROiZV39.Text, "SPR_MOUSE", frmComputers.cmbOTH)
                End If

            Case "CNT"

                If Not RSExists("OTHER", "name", Trim(frmComputers.cmbOTH.Text)) Then
                    AddOnePar(frmComputers.cmbOTH.Text, "NAME", "spr_other", frmComputers.cmbOTH)
                End If

        End Select



        If Not RSExists("otv", "name", Trim(frmComputers.cmbOTHotv.Text)) Then
            AddOnePar(frmComputers.cmbOTHotv.Text, "NAME", "SPR_OTV", frmComputers.cmbOTHotv)
        End If

        If Not RSExists("OTHD", "name", Trim(frmComputers.cmbOTH.Text)) Then
            AddTreePar(frmComputers.cmbOTH.Text, frmComputers.cmbOTHConnect.Text, frmComputers.PROiZV39.Text, "SPR_OTH_DEV", frmComputers.cmbOTH)
        End If


        Dim rs As ADODB.Recordset
        Dim unaPCL As String
        If Len(frmComputers.cmbOTHPCL.Text) <> 0 Then

            On Error GoTo sAR

            rs = New ADODB.Recordset
            rs.Open("Select id From kompy where filial='" & frmComputers.cmbOTHFil.Text & "' and mesto='" & frmComputers.cmbOTHDepart.Text & "' and kabn='" & frmComputers.cmbOTHOffice.Text & "' and NET_NAME='" & frmComputers.cmbOTHPCL.Text & "'", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

            With rs

                unaPCL = .Fields("id").Value

            End With
            rs.Close()
            rs = Nothing
        End If

sAR:
        If Len(unaPCL) = 0 Or unaPCL = Nothing Then unaPCL = 0

        'Dim rs As ADODB.Recordset
        rs = New ADODB.Recordset

        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)


        With rs
            If Len(sSID) = 0 Then
                .AddNew()
            End If

            .Fields("PRINTER_NAME_1").Value = frmComputers.cmbOTH.Text
            'sName = .Fields("PRINTER_NAME_1").Value
            .Fields("PRINTER_SN_1").Value = frmComputers.txtOTHSN.Text
            .Fields("PRINTER_PROIZV_1").Value = frmComputers.PROiZV39.Text
            .Fields("port_1").Value = frmComputers.txtOTHmemo.Text
            .Fields("OTvetstvennyj").Value = frmComputers.cmbOTHotv.Text
            .Fields("INV_NO_PRINTER").Value = frmComputers.txtOTHinnumber.Text
            .Fields("FILIAL").Value = frmComputers.cmbOTHFil.Text
            .Fields("MESTO").Value = frmComputers.cmbOTHDepart.Text
            .Fields("kabn").Value = frmComputers.cmbOTHOffice.Text
            .Fields("NET_IP_1").Value = frmComputers.txtOTHIP.Text
            .Fields("NET_MAC_1").Value = frmComputers.txtOTHMAC.Text
            .Fields("TIP_COMPA").Value = frmComputers.cmbOTHConnect.Text
            .Fields("TELEPHONE").Value = frmComputers.txtOTHphone.Text
            .Fields("TIPtehn").Value = TipTehn
            .Fields("NET_NAME").Value = frmComputers.cmbOTH.Text
            .Fields("PSEVDONIM").Value = frmComputers.cmbOTH.Text
            .Fields("PCL").Value = unaPCL

            If Len(frmComputers.txtOTHSfN.Text) = 0 Then frmComputers.txtOTHSfN.Text = 0
            If Len(frmComputers.txtOTHcash.Text) = 0 Then frmComputers.txtOTHcash.Text = 0
            If Len(frmComputers.txtOTHSumm.Text) = 0 Then frmComputers.txtOTHSumm.Text = 0



            .Fields("SFAktNo").Value = frmComputers.txtOTHSfN.Text
            .Fields("CenaRub").Value = frmComputers.txtOTHcash.Text
            .Fields("StoimRub").Value = frmComputers.txtOTHSumm.Text
            .Fields("Zaiavk").Value = frmComputers.txtOTHZay.Text

            .Fields("DataVVoda").Value = frmComputers.dtOTHdataVvoda.Value
            .Fields("dataSF").Value = frmComputers.dtOTHSFdate.Value

            .Fields("Spisan").Value = frmComputers.chkOTHspis.Checked
            .Fields("Balans").Value = frmComputers.chkOTHNNb.Checked


            .Update()
        End With
        rs.Close()
        rs = Nothing


        If frmComputers.EDT = False Then

            Dim rsBK As ADODB.Recordset
            rsBK = New ADODB.Recordset
            rsBK.Open("SELECT id FROM kompy WHERE NET_NAME='" & frmComputers.cmbOTH.Text & "' and MESTO='" & frmComputers.cmbOTHDepart.Text & "' and FILIAL='" & frmComputers.cmbOTHFil.Text & "'  and kabn='" & frmComputers.cmbOTHOffice.Text & "'", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

            With rsBK

                frmComputers.sCOUNT = .Fields("ID").Value

            End With
            rsBK.Close()
            rsBK = Nothing

            Dim objIniFile As New IniFile(PrPath & "base.ini")
            objIniFile.WriteString("general", "DK", frmComputers.sCOUNT)
            objIniFile.WriteString("general", "Default", 0)

        Else

        End If

        If Len(sSID) = 0 Then
            sSID = frmComputers.sCOUNT
        End If

        SAVE_GARANT(sSID, frmComputers.cmbOTHPostav, frmComputers.dtGOTHPr, frmComputers.dtGOTHok)

        frmComputers.cmbOTH.BackColor = frmComputers.cmbOTH.BackColor
        frmComputers.cmbOTHFil.BackColor = frmComputers.cmbOTHFil.BackColor

        If frmComputers.pDRAG = True Then

        Else
            RefFilTree(frmComputers.lstGroups)
        End If


        Exit Sub
Err_:
    End Sub

    Public Sub Save_T(Optional ByVal sSID As String = "")
        On Error GoTo err_



        If Len(frmComputers.txtSNAME.Text) = 0 Or Len(frmComputers.txtPSEUDONIM.Text) = 0 Or Len(frmComputers.cmbBranch.Text) = 0 Then

            MsgBox("Не заполнены обязательные поля", MsgBoxStyle.Information, ProGramName)

            If Len(frmComputers.txtSNAME.Text) = 0 Then frmComputers.txtSNAME.BackColor = Color.Red
            If Len(frmComputers.txtPSEUDONIM.Text) = 0 Then frmComputers.txtPSEUDONIM.BackColor = Color.Red
            If Len(frmComputers.cmbBranch.Text) = 0 Then frmComputers.cmbBranch.BackColor = Color.Red
            frmComputers.sSTAB1.SelectedTab = frmComputers.sSTAB1.TabPages("TabPage6")
            Exit Sub

        End If

        PRESAVE_TREE(frmComputers.cmbBranch, frmComputers.cmbDepartment, frmComputers.cmbOffice)


        new_prov = False

        If frmComputers.EDT = False Then

            Call proverka_sn()

        End If

        If new_prov = True Then

            Exit Sub

        End If


        Call addEXISTTEH_()


        Dim sSQL As String

        If Len(sSID) = 0 Then

            sSQL = "SELECT * FROM kompy"

        Else

            sSQL = "SELECT * FROM kompy where id=" & sSID

        End If


        If frmComputers.EDT = True Then

            Call DVIG_TEHN(frmComputers.cmbBranch.Text, frmComputers.cmbDepartment.Text, frmComputers.cmbOffice.Text, frmComputers.txtSNAME.Text)

            If DV = True Then

            Else
                If Sav = False Then
                    MsgBox("Отмена перемещения", MsgBoxStyle.Exclamation, ProGramName)

                    If frmComputers.pDRAG = True Then

                        RefFilTree(frmComputers.lstGroups)

                    End If

                    Exit Sub
                End If

            End If


        End If



        Dim rs As ADODB.Recordset

        Dim unaPCL As String
        If Len(frmComputers.cmbPCLK.Text) <> 0 Then
            On Error GoTo sAR

            rs = New ADODB.Recordset
            rs.Open("Select id From kompy where filial='" & frmComputers.cmbBranch.Text & "' and mesto='" & frmComputers.cmbDepartment.Text & "' and kabn='" & frmComputers.cmbOffice.Text & "' and NET_NAME='" & frmComputers.cmbPCLK.Text & "'", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)



            With rs
                unaPCL = .Fields("id").Value
            End With
            rs.Close()
            rs = Nothing

        End If

sAR:
        If Len(unaPCL) = 0 Or unaPCL = Nothing Then unaPCL = 0



        rs = New ADODB.Recordset

        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)


        With rs
            If Len(sSID) = 0 Then
                .AddNew()
            End If
            .Fields("CPU1").Value = frmComputers.cmbCPU1.Text
            .Fields("CPUmhz1").Value = frmComputers.txtMHZ1.Text
            .Fields("CPUSocket1").Value = frmComputers.txtSoc1.Text
            .Fields("CPUProizv1").Value = frmComputers.PROizV1.Text

            .Fields("CPU2").Value = frmComputers.cmbCPU2.Text
            .Fields("CPUmhz2").Value = frmComputers.txtMHZ2.Text
            .Fields("CPUSocket2").Value = frmComputers.txtSoc2.Text
            .Fields("CPUProizv2").Value = frmComputers.PROizV2.Text

            .Fields("CPU3").Value = frmComputers.cmbCPU3.Text
            .Fields("CPUmhz3").Value = frmComputers.txtMHZ3.Text
            .Fields("CPUSocket3").Value = frmComputers.txtSoc3.Text
            .Fields("CPUProizv3").Value = frmComputers.PROizV3.Text

            .Fields("CPU4").Value = frmComputers.cmbCPU4.Text
            .Fields("CPUmhz4").Value = frmComputers.txtMHZ4.Text
            .Fields("CPUSocket4").Value = frmComputers.txtSoc4.Text
            .Fields("CPUProizv4").Value = frmComputers.PROizV4.Text

            .Fields("Mb").Value = frmComputers.cmbMB.Text
            .Fields("Mb_Chip").Value = frmComputers.txtChip.Text
            .Fields("Mb_Proizvod").Value = frmComputers.PROizV5.Text
            .Fields("Mb_Id").Value = frmComputers.txtSN_MB.Text

            .Fields("RAM_1").Value = frmComputers.cmbRAM1.Text
            .Fields("RAM_speed_1").Value = frmComputers.txtRamS1.Text
            .Fields("RAM_SN_1").Value = frmComputers.txtRamSN1.Text
            .Fields("RAM_PROIZV_1").Value = frmComputers.PROizV6.Text

            .Fields("RAM_2").Value = frmComputers.cmbRAM2.Text
            .Fields("RAM_speed_2").Value = frmComputers.txtRamS2.Text
            .Fields("RAM_SN_2").Value = frmComputers.txtRamSN2.Text
            .Fields("RAM_PROIZV_2").Value = frmComputers.PROizV7.Text

            .Fields("RAM_3").Value = frmComputers.cmbRAM3.Text
            .Fields("RAM_speed_3").Value = frmComputers.txtRamS3.Text
            .Fields("RAM_SN_3").Value = frmComputers.txtRamSN3.Text
            .Fields("RAM_PROIZV_3").Value = frmComputers.PROizV8.Text

            .Fields("RAM_4").Value = frmComputers.cmbRAM4.Text
            .Fields("RAM_speed_4").Value = frmComputers.txtRamS4.Text
            .Fields("RAM_SN_4").Value = frmComputers.txtRamSN4.Text
            .Fields("RAM_PROIZV_4").Value = frmComputers.PROizV9.Text

            .Fields("HDD_Name_1").Value = frmComputers.cmbHDD1.Text
            .Fields("HDD_OB_1").Value = frmComputers.txtHDDo1.Text
            .Fields("HDD_SN_1").Value = frmComputers.txtHDDsN1.Text
            .Fields("HDD_PROIZV_1").Value = frmComputers.PROizV10.Text

            .Fields("HDD_Name_2").Value = frmComputers.cmbHDD2.Text
            .Fields("HDD_OB_2").Value = frmComputers.txtHDDo2.Text
            .Fields("HDD_SN_2").Value = frmComputers.txtHDDsN2.Text
            .Fields("HDD_PROIZV_2").Value = frmComputers.PROizV11.Text

            .Fields("HDD_Name_3").Value = frmComputers.cmbHDD3.Text
            .Fields("HDD_OB_3").Value = frmComputers.txtHDDo3.Text
            .Fields("HDD_SN_3").Value = frmComputers.txtHDDsN3.Text
            .Fields("HDD_PROIZV_3").Value = frmComputers.PROizV12.Text

            .Fields("HDD_Name_4").Value = frmComputers.cmbHDD4.Text
            .Fields("HDD_OB_4").Value = frmComputers.txtHDDo4.Text
            .Fields("HDD_SN_4").Value = frmComputers.txtHDDsN4.Text
            .Fields("HDD_PROIZV_4").Value = frmComputers.PROizV13.Text

            .Fields("SVGA_NAME").Value = frmComputers.cmbSVGA1.Text
            .Fields("SVGA_OB_RAM").Value = frmComputers.txtSVGAr1.Text
            .Fields("SVGA_SN").Value = frmComputers.txtSVGAs1.Text
            .Fields("SVGA_PROIZV").Value = frmComputers.PROizV14.Text


            .Fields("SVGA2_NAME").Value = frmComputers.cmbSVGA2.Text
            .Fields("SVGA2_OB_RAM").Value = frmComputers.txtSVGAr2.Text
            .Fields("SVGA2_SN").Value = frmComputers.txtSVGAs2.Text
            .Fields("SVGA2_PROIZV").Value = frmComputers.PROizV15.Text

            .Fields("SOUND_NAME").Value = frmComputers.cmbSound.Text
            .Fields("SOUND_SN").Value = frmComputers.txtSoundS.Text
            .Fields("SOUND_PROIZV").Value = frmComputers.PROizV16.Text

            .Fields("CD_NAME").Value = frmComputers.cmbOPTIC1.Text
            .Fields("CD_SPEED").Value = frmComputers.txtOPTICs1.Text
            .Fields("CD_SN").Value = frmComputers.txtOPTICsn1.Text
            .Fields("CD_PROIZV").Value = frmComputers.PROizV17.Text

            .Fields("CDRW_NAME").Value = frmComputers.cmbOPTIC2.Text
            .Fields("CDRW_SPEED").Value = frmComputers.txtOPTICs2.Text
            .Fields("CDRW_SN").Value = frmComputers.txtOPTICsn2.Text
            .Fields("CDRW_PROIZV").Value = frmComputers.PROizV18.Text

            .Fields("DVD_NAME").Value = frmComputers.cmbOPTIC3.Text
            .Fields("DVD_SPEED").Value = frmComputers.txtOPTICs3.Text
            .Fields("DVD_SN").Value = frmComputers.txtOPTICsn3.Text
            .Fields("DVD_PROIZV").Value = frmComputers.PROizV19.Text

            .Fields("NET_NAME_1").Value = frmComputers.cmbNET1.Text
            .Fields("NET_IP_1").Value = frmComputers.txtNETip1.Text
            .Fields("NET_MAC_1").Value = frmComputers.txtNETmac1.Text
            .Fields("NET_PROIZV_1").Value = frmComputers.PROizV20.Text

            .Fields("NET_NAME_2").Value = frmComputers.cmbNET2.Text
            .Fields("NET_IP_2").Value = frmComputers.txtNETip2.Text
            .Fields("NET_MAC_2").Value = frmComputers.txtNETmac2.Text
            .Fields("NET_PROIZV_2").Value = frmComputers.PROizV21.Text

            .Fields("FDD_NAME").Value = frmComputers.cmbFDD.Text
            .Fields("FDD_SN").Value = frmComputers.txtSN.Text
            '.Fields("txtFDD_").Value = frmComputers.txtFDD_.Text
            .Fields("FDD_PROIZV").Value = frmComputers.PROizV22.Text

            .Fields("MODEM_NAME").Value = frmComputers.cmbModem.Text
            .Fields("MODEM_SN").Value = frmComputers.txtModemSN.Text
            '.Fields("txtFDD_").Value = frmComputers.txtModem2.Text
            .Fields("MODEM_PROIZV").Value = frmComputers.PROizV24.Text

            .Fields("CASE_NAME").Value = frmComputers.cmbCase.Text
            .Fields("CASE_SN").Value = frmComputers.txtCase1.Text
            .Fields("CASE_PROIZV").Value = frmComputers.PROizV25.Text

            .Fields("CREADER_NAME").Value = frmComputers.cmbCreader.Text
            .Fields("CREADER_SN").Value = frmComputers.txtCreader1.Text
            .Fields("CREADER_PROIZV").Value = frmComputers.PROizV23.Text

            .Fields("BLOCK").Value = frmComputers.cmbBP.Text
            .Fields("SN_BLOCK").Value = frmComputers.txtBP1.Text
            .Fields("PROIZV_BLOCK").Value = frmComputers.PROizV26.Text

            .Fields("SYS_PR").Value = frmComputers.PROizV27.Text
            .Fields("Ser_N_SIS").Value = frmComputers.txtSNSB.Text

            'Модель системного блока
            .Fields("PATH").Value = frmComputers.txtModSB.Text

            'USB
            .Fields("USB_NAME").Value = frmComputers.cmbUSB.Text
            .Fields("USB_SN").Value = frmComputers.txtUSBSN.Text
            .Fields("USB_PROIZV").Value = frmComputers.PROizV41.Text

            'PCI
            .Fields("PCI_NAME").Value = frmComputers.cmbPCI.Text
            .Fields("PCI_SN").Value = frmComputers.txtSNPCI.Text
            .Fields("PCI_PROIZV").Value = frmComputers.PROizV42.Text


            .Fields("MONITOR_NAME").Value = frmComputers.cmbMon1.Text
            .Fields("MONITOR_DUM").Value = frmComputers.txtMon1Dum.Text
            .Fields("MONITOR_SN").Value = frmComputers.txtMon1SN.Text
            .Fields("MONITOR_PROIZV").Value = frmComputers.PROizV28.Text

            .Fields("MONITOR_NAME2").Value = frmComputers.cmbMon2.Text
            .Fields("MONITOR_DUM2").Value = frmComputers.txtMon2Dum.Text
            .Fields("MONITOR_SN2").Value = frmComputers.txtMon2SN.Text
            .Fields("MONITOR_PROIZV2").Value = frmComputers.PROizV29.Text

            .Fields("KEYBOARD_NAME").Value = frmComputers.cmbKeyb.Text
            .Fields("KEYBOARD_SN").Value = frmComputers.txtKeybSN.Text
            .Fields("KEYBOARD_PROIZV").Value = frmComputers.PROizV30.Text

            .Fields("MOUSE_NAME").Value = frmComputers.cmbMouse.Text
            .Fields("MOUSE_SN").Value = frmComputers.txtMouseSN.Text
            .Fields("MOUSE_PROIZV").Value = frmComputers.PROizV31.Text

            .Fields("AS_NAME").Value = frmComputers.cmbAsist.Text
            .Fields("AS_SN").Value = frmComputers.txtAsistSN.Text
            .Fields("AS_PROIZV").Value = frmComputers.PROizV32.Text

            .Fields("FILTR_NAME").Value = frmComputers.cmbFilter.Text
            .Fields("FILTR_SN").Value = frmComputers.txtFilterSN.Text
            .Fields("FILTR_PROIZV").Value = frmComputers.PROizV33.Text

            .Fields("IBP_NAME").Value = frmComputers.cmbIBP.Text
            .Fields("IBP_SN").Value = frmComputers.txtSNIBP.Text
            .Fields("IBP_PROIZV").Value = frmComputers.PROizV43.Text

            .Fields("PRINTER_NAME_1").Value = frmComputers.cmbPrinters1.Text
            .Fields("PRINTER_SN_1").Value = frmComputers.txtPrint1SN.Text
            .Fields("PORT_1").Value = frmComputers.txtPrint1Port.Text
            .Fields("PRINTER_PROIZV_1").Value = frmComputers.PROizV34.Text

            .Fields("PRINTER_NAME_2").Value = frmComputers.cmbPrinters2.Text
            .Fields("PRINTER_SN_2").Value = frmComputers.txtPrint2SN.Text
            .Fields("PORT_2").Value = frmComputers.txtPrint2Port.Text
            .Fields("PRINTER_PROIZV_2").Value = frmComputers.PROizV35.Text

            .Fields("PRINTER_NAME_3").Value = frmComputers.cmbPrinters3.Text
            .Fields("PRINTER_SN_3").Value = frmComputers.txtPrint3SN.Text
            .Fields("PORT_3").Value = frmComputers.txtPrint3Port.Text
            .Fields("PRINTER_PROIZV_3").Value = frmComputers.PROizV36.Text

            .Fields("NET_NAME").Value = frmComputers.txtSNAME.Text
            .Fields("PSEVDONIM").Value = frmComputers.txtPSEUDONIM.Text

            .Fields("FILIAL").Value = frmComputers.cmbBranch.Text
            .Fields("MESTO").Value = frmComputers.cmbDepartment.Text
            .Fields("kabn").Value = frmComputers.cmbOffice.Text

            .Fields("OTvetstvennyj").Value = frmComputers.cmbResponsible.Text
            .Fields("TELEPHONE").Value = frmComputers.txtPHONE.Text
            .Fields("TIP_COMPA").Value = frmComputers.cmbAppointment.Text

            .Fields("INV_NO_SYSTEM").Value = frmComputers.txtSBSN.Text
            .Fields("INV_NO_MONITOR").Value = frmComputers.txtMSN.Text
            .Fields("INV_NO_IBP").Value = frmComputers.IN_IBP.Text
            .Fields("INV_NO_PRINTER").Value = frmComputers.IN_PRN.Text
            .Fields("PCL").Value = unaPCL

            .Fields("TIPtehn").Value = TipTehn


            If Len(frmComputers.txtPCcash.Text) = 0 Then frmComputers.txtPCcash.Text = 0
            If Len(frmComputers.txtPCSumm.Text) = 0 Then frmComputers.txtPCSumm.Text = 0
            If Len(frmComputers.txtPCSfN.Text) = 0 Then frmComputers.txtPCSfN.Text = 0

            .Fields("SFAktNo").Value = frmComputers.txtPCSfN.Text
            .Fields("CenaRub").Value = frmComputers.txtPCcash.Text

            .Fields("StoimRub").Value = frmComputers.txtPCSumm.Text

            .Fields("Zaiavk").Value = frmComputers.txtPCZay.Text

            .Fields("DataVVoda").Value = frmComputers.dtPCdataVvoda.Value
            .Fields("dataSF").Value = frmComputers.dtPCSFdate.Value

            .Fields("Spisan").Value = frmComputers.chkPCspis.Checked
            .Fields("Balans").Value = frmComputers.chkPCNNb.Checked

            .Fields("Garantia_Sist").Value = frmComputers.rbSist.Checked


            .Update()
        End With

        rs.Close()
        rs = Nothing


        If frmComputers.EDT = False Then

            Dim rsBK As ADODB.Recordset
            rsBK = New ADODB.Recordset
            rsBK.Open("SELECT id FROM kompy WHERE NET_NAME='" & frmComputers.txtSNAME.Text & "' and MESTO='" & frmComputers.cmbDepartment.Text & "' and FILIAL='" & frmComputers.cmbBranch.Text & "'  and kabn='" & frmComputers.cmbOffice.Text & "'", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

            With rsBK

                frmComputers.sCOUNT = .Fields("ID").Value

            End With
            rsBK.Close()
            rsBK = Nothing

            Dim objIniFile As New IniFile(PrPath & "base.ini")
            objIniFile.WriteString("general", "DK", frmComputers.sCOUNT)
            objIniFile.WriteString("general", "Default", 0)

        Else

            sSQL = "SELECT count(*) as t_n FROM kompy where PCL=" & frmComputers.sCOUNT
            rs = New ADODB.Recordset
            rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

            Dim sCON As String

            With rs
                sCON = .Fields("t_n").Value
            End With
            rs.Close()
            rs = Nothing

            If sCON > 0 Then

                rs = New ADODB.Recordset
                rs.Open("update kompy set OTvetstvennyj='" & frmComputers.cmbResponsible.Text & "', FILIAL='" & frmComputers.cmbBranch.Text & "', mesto='" & frmComputers.cmbDepartment.Text & "', kabn='" & frmComputers.cmbOffice.Text & "' WHERE PCL=" & frmComputers.sCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                'rs.Close()
                rs = Nothing

            End If


        End If



        If Len(sSID) = 0 Then
            sSID = frmComputers.sCOUNT
        End If

        If frmComputers.rbSist.Checked = True Then

            SAVE_GARANT(sSID, frmComputers.cmbPostav, frmComputers.dtGPr, frmComputers.dtGok)

        Else


        End If


        'frmComputers.Invoke(New MethodInvoker(AddressOf S_P_LOAD))


        If frmComputers.EDT = False Then

            Call SAVE_SOFT(frmComputers.sCOUNT)

        Else

        End If


        If frmComputers.MASSLOAD = True Then


        Else

            frmComputers.txtSNAME.BackColor = frmComputers.txtSBSN.BackColor
            frmComputers.txtPSEUDONIM.BackColor = frmComputers.txtSBSN.BackColor
            frmComputers.cmbBranch.BackColor = frmComputers.txtSBSN.BackColor

            If frmComputers.pDRAG = True Then

            Else
                RefFilTree(frmComputers.lstGroups)
            End If


        End If


        Exit Sub
err_:
        MsgBox(Err.Description)


    End Sub

    'Private Sub S_P_LOAD()

    '    If frmComputers.InvokeRequired Then
    '        frmComputers.Invoke(New MethodInvoker(AddressOf S_P_LOAD))

    '    Else
    '        Call SAVE_SOFT(frmComputers.sCOUNT)
    '    End If
    'End Sub


    Public Sub SAVE_SOFT(Optional ByVal sSID As Integer = 0)
        On Error Resume Next

        If sSID = 0 Then Exit Sub



        Dim A1, B1, C1, F1, G1, H1 As String
        Dim D1, E1 As Date
        Dim I1 As String
        Dim intj As Integer

        Dim uname34 As String

        For intj = 0 To frmComputers.lstSoftware.Items.Count - 1

            If frmComputers.lstSoftware.Items(intj).SubItems(6).Text <> "" Or frmComputers.lstSoftware.Items(intj).SubItems(6).Text <> "<N/A>" Or Len(frmComputers.lstSoftware.Items(intj).SubItems(6).Text) <> 0 Then
                uname34 = frmComputers.lstSoftware.Items(intj).SubItems(6).Text
            Else
                uname34 = "NoName"
            End If

            If Not (RSExists("PROYZV", "PROiZV", Trim(uname34))) Then
                AddPr(Trim(uname34))
            End If
        Next

        Dim rsSoft As ADODB.Recordset

        For intj = 0 To frmComputers.lstSoftware.Items.Count - 1

            If Len(frmComputers.lstSoftware.Items(intj).SubItems(1).Text) > 0 Then
                frmComputers.lstSoftware.Items(intj).Selected = True
                frmComputers.lstSoftware.Items(intj).EnsureVisible()
                'frmComputers.lstSoftware.SetFocus()


                frmComputers.lstSoftware.Items(intj).Focused = True

                If Len(frmComputers.lstSoftware.Items(intj).Text) = 0 Then frmComputers.lstSoftware.Items(intj).Text = 1
                A1 = frmComputers.lstSoftware.Items(intj).Text


                H1 = frmComputers.lstSoftware.Items(intj).SubItems(2).Text

                If Len(frmComputers.lstSoftware.Items(intj).SubItems(3).Text) = 0 Then
                    B1 = ""
                Else
                    B1 = frmComputers.lstSoftware.Items(intj).SubItems(3).Text
                End If

                If Len(frmComputers.lstSoftware.Items(intj).SubItems(4).Text) = 0 Then
                    C1 = ""
                Else
                    C1 = frmComputers.lstSoftware.Items(intj).SubItems(4).Text
                End If

                If Len(frmComputers.lstSoftware.Items(intj).SubItems(5).Text) = 0 Then
                    D1 = Date.Today
                Else
                    D1 = frmComputers.lstSoftware.Items(intj).SubItems(5).Text
                End If

                If Len(frmComputers.lstSoftware.Items(intj).SubItems(6).Text) = 0 Then
                    E1 = Date.Today
                Else
                    E1 = frmComputers.lstSoftware.Items(intj).SubItems(6).Text
                End If

                If Len(frmComputers.lstSoftware.Items(intj).SubItems(7).Text) = 0 Then
                    F1 = ""
                Else
                    F1 = frmComputers.lstSoftware.Items(intj).SubItems(7).Text
                    If F1 = "<N/A>" Then F1 = ""
                End If

                If Len(frmComputers.lstSoftware.Items(intj).SubItems(8).Text) = 0 Then
                    G1 = ""
                Else '
                    G1 = frmComputers.lstSoftware.Items(intj).SubItems(9).Text
                End If



                I1 = sSID

                If Not (RSExistsSoft(sSID, H1)) Then
                    If Len(H1) > 1 Then

                        rsSoft = New ADODB.Recordset
                        rsSoft.Open("SELECT * FROM SOFT_INSTALL", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        With rsSoft
                            .AddNew()
                            .Fields("Soft").Value = H1 '.Text
                            .Fields("NomerSoftKomp").Value = A1
                            .Fields("t_lic").Value = C1
                            .Fields("L_key").Value = B1
                            .Fields("d_p").Value = D1
                            .Fields("d_o").Value = E1
                            .Fields("Publisher").Value = F1
                            .Fields("TIP").Value = ""
                            .Fields("Id_Comp").Value = I1
                            .Update()

                        End With

                        rsSoft.Close()
                        rsSoft = Nothing
                    End If
                End If
            End If
        Next

        'Костыль для MyODBC 5
        rsSoft = New ADODB.Recordset
        rsSoft.Open("UPDATE SOFT_INSTALL SET Id_Comp=" & I1 & " WHERE Id_Comp='0'", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        rsSoft.Close()
        rsSoft = Nothing


    End Sub

    Public Sub Save_P(Optional ByVal sSID As String = "")

        On Error GoTo Err_



        If Len(frmComputers.cmbPRN.Text) = 0 Or Len(frmComputers.cmbPRNFil.Text) = 0 Then

            MsgBox("Не заполнены обязательные поля", MsgBoxStyle.Information, ProGramName)

            If Len(frmComputers.cmbPRN.Text) = 0 Then frmComputers.cmbPRN.BackColor = Color.Red
            If Len(frmComputers.cmbPRNFil.Text) = 0 Then frmComputers.cmbPRNFil.BackColor = Color.Red

            frmComputers.sSTAB2.SelectedTab = frmComputers.sSTAB3.TabPages("TabPage9")
            Exit Sub
        End If

        If frmComputers.EDT = True Then
            Call DVIG_TEHN(frmComputers.cmbPRNFil.Text, frmComputers.cmbPRNDepart.Text, frmComputers.cmbPRNOffice.Text, frmComputers.cmbPRN.Text)

            If DV = True Then

            Else
                If Sav = False Then
                    MsgBox("Отмена перемещения", MsgBoxStyle.Exclamation, ProGramName)
                    Exit Sub
                End If

            End If

        End If

        Dim sSQL As String

        If Len(sSID) = 0 Then

            sSQL = "SELECT * FROM kompy"

        Else

            sSQL = "SELECT * FROM kompy where id=" & sSID

        End If

        If Not (RSExists("CARTR", "name", frmComputers.cmbModCartr.Text)) Then
            AddTwoPar(frmComputers.cmbModCartr.Text, frmComputers.PROiZV38.Text, "spr_cart", frmComputers.cmbModCartr)
        End If

        Select Case TipTehn

            Case "MFU"
                If Not (RSExists("MFU", "name", frmComputers.cmbPRN.Text)) Then
                    AddFOwPar(frmComputers.cmbPRN.Text, frmComputers.cmbFormat.Text, frmComputers.cmbModCartr.Text, frmComputers.PROiZV38.Text, "SPR_MFU", frmComputers.cmbPRN)
                End If

            Case "Printer"

                If Not (RSExists("PRINTERY", "name", frmComputers.cmbPRN.Text)) Then
                    AddFOwPar(frmComputers.cmbPRN.Text, frmComputers.cmbFormat.Text, frmComputers.cmbModCartr.Text, frmComputers.PROiZV38.Text, "SPR_PRINTER", frmComputers.cmbPRN)
                End If

            Case "KOpir"

                If Not (RSExists("KOPIRY", "name", frmComputers.cmbPRN.Text)) Then
                    AddFOwPar(frmComputers.cmbPRN.Text, frmComputers.cmbFormat.Text, frmComputers.cmbModCartr.Text, frmComputers.PROiZV38.Text, "SPR_KOPIR", frmComputers.cmbPRN)
                End If

        End Select

        If Not (RSExists("otv", "name", Trim(frmComputers.cmbPRNotv.Text))) Then
            AddOnePar(frmComputers.cmbPRNotv.Text, "NAME", "SPR_OTV", frmComputers.cmbPRNotv)
        End If


        PRESAVE_TREE(frmComputers.cmbPRNFil, frmComputers.cmbPRNDepart, frmComputers.cmbPRNOffice)



        Dim rs As ADODB.Recordset
        Dim unaPCL As String
        If Len(frmComputers.cmbPCL.Text) <> 0 Then

            On Error GoTo sAR

            rs = New ADODB.Recordset
            'rs.Open("Select id From kompy where filial='" & frmComputers.cmbPRNFil.Text & "' and mesto='" & frmComputers.cmbPRNDepart.Text & "' and kabn='" & frmComputers.cmbPRNOffice.Text & "' and TipTehn='PC' and NET_NAME='" & frmComputers.cmbPCL.Text & "'", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            rs.Open("Select id From kompy where filial='" & frmComputers.cmbPRNFil.Text & "' and mesto='" & frmComputers.cmbPRNDepart.Text & "' and kabn='" & frmComputers.cmbPRNOffice.Text & "' and NET_NAME='" & frmComputers.cmbPCL.Text & "'", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            With rs

                unaPCL = .Fields("id").Value

            End With
            rs.Close()
            rs = Nothing
        End If

sAR:
        If Len(unaPCL) = 0 Or unaPCL = Nothing Then unaPCL = 0

        On Error GoTo Err_

        rs = New ADODB.Recordset
        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)


        With rs
            If Len(sSID) = 0 Then
                .AddNew()
                .Fields("date").Value = Date.Today
                .Fields("TIPtehn").Value = TipTehn
            End If

            .Fields("PRINTER_NAME_1").Value = frmComputers.cmbPRN.Text
            .Fields("PRINTER_SN_1").Value = frmComputers.txtPRNSN.Text
            .Fields("PRINTER_PROIZV_1").Value = frmComputers.PROiZV38.Text
            .Fields("INV_NO_PRINTER").Value = frmComputers.txtPRNinnumber.Text
            .Fields("port_1").Value = frmComputers.cmbFormat.Text
            .Fields("FILIAL").Value = frmComputers.cmbPRNFil.Text
            .Fields("mesto").Value = frmComputers.cmbPRNDepart.Text
            .Fields("kabn").Value = frmComputers.cmbPRNOffice.Text
            .Fields("os").Value = frmComputers.cmbModCartr.Text
            .Fields("OTvetstvennyj").Value = frmComputers.cmbPRNotv.Text
            .Fields("TELEPHONE").Value = frmComputers.txtPRNphone.Text
            .Fields("NET_IP_1").Value = frmComputers.txtPrnIP.Text
            .Fields("NET_MAC_1").Value = frmComputers.txtPRNMAC.Text
            .Fields("OS").Value = frmComputers.cmbModCartr.Text
            .Fields("NET_NAME").Value = frmComputers.cmbPRN.Text
            .Fields("PSEVDONIM").Value = frmComputers.cmbPRN.Text
            .Fields("PCL").Value = unaPCL

            .Fields("port_2").Value = frmComputers.cmbPRNConnect.Text
            'cmbPRNConnect
            'unaPCL
            If Len(frmComputers.txtPRNSfN.Text) = 0 Then frmComputers.txtPRNSfN.Text = 0
            If Len(frmComputers.txtPRNcash.Text) = 0 Then frmComputers.txtPRNcash.Text = 0
            If Len(frmComputers.txtPRNSumm.Text) = 0 Then frmComputers.txtPRNSumm.Text = 0

            .Fields("SFAktNo").Value = frmComputers.txtPRNSfN.Text
            .Fields("CenaRub").Value = frmComputers.txtPRNcash.Text
            .Fields("StoimRub").Value = frmComputers.txtPRNSumm.Text

            .Fields("Zaiavk").Value = frmComputers.txtPRNZay.Text

            .Fields("DataVVoda").Value = frmComputers.dtPRNdataVvoda.Value
            .Fields("dataSF").Value = frmComputers.dtPRNSFdate.Value

            .Fields("Spisan").Value = frmComputers.chkPRNspis.Checked
            .Fields("Balans").Value = frmComputers.chkPRNNNb.Checked

            .Update()
        End With

        rs.Close()
        rs = Nothing

        If frmComputers.EDT = False Then

            Dim rsBK As ADODB.Recordset
            rsBK = New ADODB.Recordset
            rsBK.Open("SELECT id FROM kompy WHERE NET_NAME='" & frmComputers.cmbPRN.Text & "' and MESTO='" & frmComputers.cmbPRNDepart.Text & "' and FILIAL='" & frmComputers.cmbPRNFil.Text & "'  and kabn='" & frmComputers.cmbPRNOffice.Text & "'", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

            With rsBK

                frmComputers.sCOUNT = .Fields("ID").Value

            End With
            rsBK.Close()
            rsBK = Nothing

            Dim objIniFile As New IniFile(PrPath & "base.ini")
            objIniFile.WriteString("general", "DK", frmComputers.sCOUNT)
            objIniFile.WriteString("general", "Default", 0)

        Else

        End If

        If Len(sSID) = 0 Then
            sSID = frmComputers.sCOUNT
        End If

        SAVE_GARANT(sSID, frmComputers.cmbPRNPostav, frmComputers.dtGPRNPr, frmComputers.dtGPRNok)


        frmComputers.cmbPRN.BackColor = frmComputers.cmbPRN.BackColor
        frmComputers.cmbPRNFil.BackColor = frmComputers.cmbPRNFil.BackColor

        If frmComputers.pDRAG = True Then

        Else
            RefFilTree(frmComputers.lstGroups)
        End If

        Exit Sub
Err_:
    End Sub

    Public Sub Save_NET(Optional ByVal sSID As String = "")

        On Error Resume Next

        If Len(frmComputers.cmbDevNet.Text) = 0 Or Len(frmComputers.cmbNETBranch.Text) = 0 Then

            MsgBox("Не заполнены обязательные поля", MsgBoxStyle.Information, ProGramName)

            If Len(frmComputers.cmbDevNet.Text) = 0 Then frmComputers.cmbDevNet.BackColor = Color.Red
            If Len(frmComputers.cmbNETBranch.Text) = 0 Then frmComputers.cmbNETBranch.BackColor = Color.Red
            frmComputers.sSTAB4.SelectedTab = frmComputers.sSTAB4.TabPages("TabPage1")
            Exit Sub

        End If

        If frmComputers.EDT = True Then
            Call DVIG_TEHN(frmComputers.cmbNETBranch.Text, frmComputers.cmbNetDepart.Text, frmComputers.cmbNETOffice.Text, frmComputers.cmbDevNet.Text)

            If DV = True Then

            Else
                If Sav = False Then
                    MsgBox("Отмена перемещения", MsgBoxStyle.Exclamation, ProGramName)
                    Exit Sub
                End If

            End If

        End If



        Dim rs As ADODB.Recordset

        Dim unaPCL As String
        If Len(frmComputers.cmbCNTNet.Text) <> 0 Then
            On Error GoTo sAR

            rs = New ADODB.Recordset
            rs.Open("Select id From kompy where filial='" & frmComputers.cmbNETBranch.Text & "' and mesto='" & frmComputers.cmbNetDepart.Text & "' and kabn='" & frmComputers.cmbNETOffice.Text & "' and NET_NAME='" & frmComputers.cmbCNTNet.Text & "'", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

            With rs
                unaPCL = .Fields("id").Value
            End With
            rs.Close()
            rs = Nothing

        End If

sAR:
        If Len(unaPCL) = 0 Or unaPCL = Nothing Then unaPCL = 0


        Dim sSQL As String 'Переменная, где будет размещён SQL запрос


        If frmComputers.EDT = False Then

            sSQL = "SELECT * FROM kompy"

        Else

            sSQL = "SELECT * FROM kompy WHERE id =" & sSID
        End If

        If Not (RSExists("otv", "name", Trim(frmComputers.cmbNETotv.Text))) Then
            AddOnePar(frmComputers.cmbNETotv.Text, "NAME", "SPR_OTV", frmComputers.cmbNETotv)
        End If

        If Not (RSExists("DNDEV", "name", frmComputers.cmbNetDev.Text)) Then
            AddOnePar(frmComputers.cmbNetDev.Text, "NAME", "SPR_NET_DEV", frmComputers.cmbNetDev)
        End If

        If Not (RSExists("NDEV", "name", frmComputers.cmbDevNet.Text)) Then
            AddFOwPar(frmComputers.cmbDevNet.Text, frmComputers.cmbNetDev.Text, frmComputers.txtNetPort.Text, frmComputers.PROiZV40.Text, "SPR_DEV_NET", frmComputers.cmbCPU2)
        End If

        PRESAVE_TREE(frmComputers.cmbNETBranch, frmComputers.cmbNetDepart, frmComputers.cmbNETOffice)


        rs = New ADODB.Recordset
        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        With rs
            If Len(sSID) = 0 Then
                .AddNew()
            End If

            .Fields("PRINTER_NAME_1").Value = frmComputers.cmbNetDev.Text
            .Fields("PRINTER_SN_1").Value = frmComputers.cmbDevNet.Text

            .Fields("PRINTER_PROIZV_1").Value = frmComputers.PROiZV40.Text

            .Fields("PRINTER_NAME_2").Value = frmComputers.txtNetIP.Text
            .Fields("PRINTER_PROIZV_2").Value = frmComputers.txtNetMac.Text
            .Fields("PRINTER_SN_2").Value = frmComputers.txtNetPort.Text

            .Fields("PRINTER_PROIZV_3").Value = frmComputers.txtNetINN.Text
            .Fields("PRINTER_SN_3").Value = frmComputers.txtNetIsp.Text

            .Fields("PRINTER_NAME_4").Value = frmComputers.cmbNetVkl.Text
            .Fields("PRINTER_PROIZV_4").Value = frmComputers.cmbNetCable.Text
            .Fields("PRINTER_SN_4").Value = frmComputers.txtNetCableCat.Text

            '.Fields("OTvetstvennyj").Value = frmComputers.txtNetNumberPorts.Text
            '.Fields("telephone").Value = frmComputers.cmbNETotv.Text

            .Fields("OTvetstvennyj").Value = frmComputers.cmbNETotv.Text
            .Fields("telephone").Value = frmComputers.txtNETphone.Text
            .Fields("port_2").Value = frmComputers.txtNetNumberPorts.Text

            .Fields("filial").Value = frmComputers.cmbNETBranch.Text
            .Fields("mesto").Value = frmComputers.cmbNetDepart.Text
            .Fields("kabn").Value = frmComputers.cmbNETOffice.Text
            'sBranch = .Fields("FILIAL").Value
            'sDepartment = .Fields("MESTO").Value
            'sOffice = .Fields("kabn").Value
            .Fields("port_1").Value = frmComputers.txtNetSN.Text
            .Fields("TIPtehn").Value = TipTehn

            .Fields("PSEVDONIM").Value = frmComputers.cmbNetDev.Text
            .Fields("NET_NAME").Value = frmComputers.cmbDevNet.Text

            .Fields("PCL").Value = unaPCL

            If Len(frmComputers.txtNETSfN.Text) = 0 Then frmComputers.txtNETSfN.Text = 0
            If Len(frmComputers.txtNETcash.Text) = 0 Then frmComputers.txtNETcash.Text = 0
            If Len(frmComputers.txtNETSumm.Text) = 0 Then frmComputers.txtNETSumm.Text = 0

            .Fields("SFAktNo").Value = frmComputers.txtNETSfN.Text
            .Fields("CenaRub").Value = frmComputers.txtNETcash.Text
            .Fields("StoimRub").Value = frmComputers.txtNETSumm.Text
            .Fields("Zaiavk").Value = frmComputers.txtNETZay.Text

            .Fields("DataVVoda").Value = frmComputers.dtNETdataVvoda.Value
            .Fields("dataSF").Value = frmComputers.dtNETSFdate.Value

            .Fields("Spisan").Value = frmComputers.chkNETspis.Checked
            .Fields("Balans").Value = frmComputers.chkNETNNb.Checked

            .Update()
        End With
        rs.Close()
        rs = Nothing

        If frmComputers.EDT = False Then

            Dim rsBK As ADODB.Recordset
            rsBK = New ADODB.Recordset
            rsBK.Open("SELECT id FROM kompy WHERE NET_NAME='" & frmComputers.cmbDevNet.Text & "' and MESTO='" & frmComputers.cmbNetDepart.Text & "' and FILIAL='" & frmComputers.cmbNETBranch.Text & "'  and kabn='" & frmComputers.cmbNETOffice.Text & "'", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            With rsBK

                frmComputers.sCOUNT = .Fields("ID").Value
            End With
            rsBK.Close()
            rsBK = Nothing

            Dim objIniFile As New IniFile(PrPath & "base.ini")
            objIniFile.WriteString("general", "DK", frmComputers.sCOUNT)
            objIniFile.WriteString("general", "Default", 0)
        Else
        End If

        If Len(sSID) = 0 Then
            sSID = frmComputers.sCOUNT
        End If

        SAVE_GARANT(sSID, frmComputers.cmbNETPostav, frmComputers.dtGNETPr, frmComputers.dtGNETok)

        frmComputers.cmbDevNet.BackColor = frmComputers.txtSBSN.BackColor
        frmComputers.cmbNETBranch.BackColor = frmComputers.txtSBSN.BackColor

        If frmComputers.pDRAG = True Then

        Else
            RefFilTree(frmComputers.lstGroups)
        End If

    End Sub

    Private Sub DVIG_TEHN(ByVal sFIALIAL As String, ByVal sOTDEL As String, ByVal sKABN As String, ByVal sNAMEs As String)
        On Error GoTo Error_
        Dim rs As ADODB.Recordset
        Dim Message, Title, Defaults As String
        Dim strTmp As String
        Dim sTmp As DateTime = DateTime.Now

        'sNAMEs - переданное имя 



        Dim iA, iB, iC As String


        If Len(sName) = 0 Then

        Else
            If sName <> sNAMEs And frmComputers.EDT = True Then

                rs = New ADODB.Recordset
                rs.Open("update Remont set Comp_Name ='" & sNAMEs & _
        "', Mesto_Compa ='" & sBranch & "/" & sDepartment & _
        "' WHERE Id_Comp=" & frmComputers.sCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                rs = Nothing

            Else

            End If

        End If


        If frmComputers.EDT = True Then

            DV = True


            rs = New ADODB.Recordset
            rs.Open("SELECT * FROM kompy where id =" & frmComputers.sCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

            With rs
                iA = .Fields("filial").Value
                iB = .Fields("mesto").Value
                iC = .Fields("kabn").Value
            End With
            rs.Close()
            rs = Nothing



            If iB <> sOTDEL Or iA <> sFIALIAL Or iC <> sKABN Then

                DV = False

                Message = "Укажите причину перемещения техники"
                Title = "Перемещение техники"
                Defaults = "Причина"
                strTmp = InputBox(Message, Title, Defaults)


                If strTmp = "" Then

                    Sav = False

                    Exit Sub
                Else
                    Sav = True
                    'Do something with the input
                End If


                rs = New ADODB.Recordset
                rs.Open("SELECT * FROM dvig", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                'sTmp = (DateTime.Now.Hour & ":" & DateTime.Now.Minute & ":" & DateTime.Now.Second)


                With rs
                    .AddNew()
                    .Fields("id_comp").Value = frmComputers.sCOUNT
                    .Fields("oldMesto").Value = iA & "/" & iB & "/" & iC
                    .Fields("NewMesto").Value = sFIALIAL & "/" & sOTDEL & "/" & sKABN
                    .Fields("prich").Value = strTmp
                    .Fields("data").Value = Date.Today
                    .Fields("time").Value = sTmp.ToLongTimeString
                    .Update()
                End With
                rs.Close()
                rs = Nothing
                'Call SaveActivityToLogDB("Перемещение техники " & frmComputers.SelNde.Text & " из " & frmComputers.FilD & "/" & frmComputers.OtdD & " в " & sfilial & "/" & sOTDEL)

                Dim langfile As New IniFile(sLANGPATH)
                Call SaveActivityToLogDB(langfile.GetString("frmComputers", "MSG52", "") & " " & frmComputers.lstGroups.SelectedNode.Text)




                rs = New ADODB.Recordset
                rs.Open("SELECT count(*) as t_n FROM kompy where PCL=" & frmComputers.sCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                Dim sCN As String

                With rs
                    sCN = .Fields("t_n").Value
                End With
                rs.Close()
                rs = Nothing

                If sCN > 0 Then
                    rs = New ADODB.Recordset
                    rs.Open("SELECT * FROM kompy where PCL=" & frmComputers.sCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                    Dim rs1 As ADODB.Recordset

                    With rs
                        .MoveFirst()
                        Do While Not .EOF
                            sCN = .Fields("id").Value

                            rs1 = New ADODB.Recordset
                            rs1.Open("SELECT * FROM dvig", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                            With rs1
                                .AddNew()
                                .Fields("id_comp").Value = sCN
                                .Fields("oldMesto").Value = iA & "/" & iB & "/" & iC
                                .Fields("NewMesto").Value = sFIALIAL & "/" & sOTDEL & "/" & sKABN
                                .Fields("prich").Value = strTmp
                                .Fields("data").Value = Date.Today
                                .Fields("time").Value = sTmp.ToLongTimeString
                                .Update()
                            End With
                            rs1.Close()
                            rs1 = Nothing




                            .MoveNext()
                        Loop
                    End With
                    rs.Close()
                    rs = Nothing

                   
                End If



            Else

            End If

        Else

        End If


        Exit Sub
Error_:
        MsgBox(Err.Description, vbInformation, ProGramName)
    End Sub

    Public Sub addEXISTTEH_()

        'Postav


        If Not (RSExists("CPUS", "name", frmComputers.cmbCPU1.Text)) Then
            AddFOwPar(frmComputers.cmbCPU1.Text, frmComputers.txtMHZ1.Text, frmComputers.txtSoc1.Text, frmComputers.PROizV1.Text, "SPR_CPU", frmComputers.cmbCPU1)
        End If

        If Not (RSExists("CPUS", "name", frmComputers.cmbCPU2.Text)) Then
            AddFOwPar(frmComputers.cmbCPU2.Text, frmComputers.txtMHZ2.Text, frmComputers.txtSoc2.Text, frmComputers.PROizV2.Text, "SPR_CPU", frmComputers.cmbCPU2)
        End If

        If Not (RSExists("MB", "name", frmComputers.cmbMB.Text)) Then
            AddTreePar(frmComputers.cmbMB.Text, frmComputers.txtChip.Text, frmComputers.PROizV5.Text, "SPR_MB", frmComputers.cmbMB)
        End If

        If Not (RSExists("RAM", "name", frmComputers.cmbRAM1.Text)) Then
            AddTreePar(frmComputers.cmbRAM1.Text, frmComputers.txtRamS1.Text, frmComputers.PROizV6.Text, "SPR_RAM", frmComputers.cmbRAM1)
        End If

        If Not (RSExists("RAM", "name", frmComputers.cmbRAM2.Text)) Then
            AddTreePar(frmComputers.cmbRAM2.Text, frmComputers.txtRamS2.Text, frmComputers.PROizV7.Text, "SPR_RAM", frmComputers.cmbRAM2)
        End If

        If Not (RSExists("RAM", "name", frmComputers.cmbRAM3.Text)) Then
            AddTreePar(frmComputers.cmbRAM3.Text, frmComputers.txtRamS3.Text, frmComputers.PROizV8.Text, "SPR_RAM", frmComputers.cmbRAM3)
        End If

        If Not (RSExists("RAM", "name", frmComputers.cmbRAM4.Text)) Then
            AddTreePar(frmComputers.cmbRAM4.Text, frmComputers.txtRamS4.Text, frmComputers.PROizV9.Text, "SPR_RAM", frmComputers.cmbRAM4)
        End If

        If Not (RSExists("HDDS", "name", frmComputers.cmbHDD1.Text)) Then
            AddTreePar(frmComputers.cmbHDD1.Text, frmComputers.txtHDDo1.Text, frmComputers.PROizV10.Text, "SPR_HDD", frmComputers.cmbHDD1)
        End If

        If Not (RSExists("HDDS", "name", frmComputers.cmbHDD2.Text)) Then
            AddTreePar(frmComputers.cmbHDD2.Text, frmComputers.txtHDDo2.Text, frmComputers.PROizV11.Text, "SPR_HDD", frmComputers.cmbHDD2)
        End If

        If Not (RSExists("HDDS", "name", frmComputers.cmbHDD3.Text)) Then
            AddTreePar(frmComputers.cmbHDD3.Text, frmComputers.txtHDDo3.Text, frmComputers.PROizV12.Text, "SPR_HDD", frmComputers.cmbHDD3)
        End If

        If Not (RSExists("HDDS", "name", frmComputers.cmbHDD4.Text)) Then
            AddTreePar(frmComputers.cmbHDD4.Text, frmComputers.txtHDDo4.Text, frmComputers.PROizV13.Text, "SPR_HDD", frmComputers.cmbHDD4)
        End If

        If Not (RSExists("SVGA", "name", frmComputers.cmbSVGA1.Text)) Then
            AddTreePar(frmComputers.cmbSVGA1.Text, frmComputers.txtSVGAr1.Text, frmComputers.PROizV14.Text, "SPR_SVGA", frmComputers.cmbSVGA1)
        End If

        If Not (RSExists("SOUND", "name", frmComputers.cmbSound.Text)) Then
            AddTwoPar(frmComputers.cmbSound.Text, frmComputers.PROizV16.Text, "SPR_SOUND", frmComputers.cmbSound)
        End If

        If Not (RSExists("CDROMS", "name", frmComputers.cmbOPTIC1.Text)) Then
            AddTreePar(frmComputers.cmbOPTIC1.Text, frmComputers.txtOPTICs1.Text, frmComputers.PROizV17.Text, "SPR_OPTICAL", frmComputers.cmbOPTIC1)
        End If

        If Not (RSExists("CDRWS", "name", frmComputers.cmbOPTIC2.Text)) Then
            AddTreePar(frmComputers.cmbOPTIC2.Text, frmComputers.txtOPTICs2.Text, frmComputers.PROizV18.Text, "SPR_OPTICAL", frmComputers.cmbOPTIC2)
        End If

        If Not (RSExists("DVDROMS", "name", frmComputers.cmbOPTIC3.Text)) Then
            AddTreePar(frmComputers.cmbOPTIC3.Text, frmComputers.txtOPTICs3.Text, frmComputers.PROizV19.Text, "SPR_OPTICAL", frmComputers.cmbOPTIC3)
        End If


        If Not (RSExists("NET", "name", frmComputers.cmbNET1.Text)) Then
            AddTwoPar(frmComputers.cmbNET1.Text, frmComputers.PROizV20.Text, "SPR_NET", frmComputers.cmbNET1)
        End If


        If Not (RSExists("NET", "name", frmComputers.cmbNET2.Text)) Then
            AddTwoPar(frmComputers.cmbNET2.Text, frmComputers.PROizV21.Text, "SPR_NET", frmComputers.cmbNET2)
        End If


        If Not (RSExists("FDDs", "name", frmComputers.cmbFDD.Text)) Then
            AddTwoPar(frmComputers.cmbFDD.Text, frmComputers.PROizV22.Text, "SPR_FDD", frmComputers.cmbFDD)
        End If


        If Not (RSExists("MODEM", "name", frmComputers.cmbModem.Text)) Then
            AddTwoPar(frmComputers.cmbModem.Text, frmComputers.PROizV24.Text, "SPR_MODEM", frmComputers.cmbModem)
        End If


        If Not (RSExists("KEYBOARD", "name", frmComputers.cmbKeyb.Text)) Then
            AddTwoPar(frmComputers.cmbKeyb.Text, frmComputers.PROizV30.Text, "SPR_KEYBOARD", frmComputers.cmbKeyb)
        End If


        If Not (RSExists("MOUSE", "name", frmComputers.cmbMouse.Text)) Then
            AddTwoPar(frmComputers.cmbMouse.Text, frmComputers.PROizV31.Text, "SPR_MOUSE", frmComputers.cmbMouse)
        End If


        If Not (RSExists("USB", "name", frmComputers.cmbUSB.Text)) Then
            AddTwoPar(frmComputers.cmbUSB.Text, frmComputers.PROizV41.Text, "SPR_USB", frmComputers.cmbUSB)
        End If

        If Not (RSExists("MONITOR", "name", frmComputers.cmbMon1.Text)) Then
            AddTreePar(frmComputers.cmbMon1.Text, frmComputers.txtMon1Dum.Text, frmComputers.PROizV28.Text, "SPR_MONITOR", frmComputers.cmbMon1)
        End If

        If Not (RSExists("MONITOR", "name", frmComputers.cmbMon2.Text)) Then
            AddTreePar(frmComputers.cmbMon2.Text, frmComputers.txtMon2Dum.Text, frmComputers.PROizV29.Text, "SPR_MONITOR", frmComputers.cmbMon2)
        End If

        If Not (RSExists("ASISTEM", "name", frmComputers.cmbAsist.Text)) Then
            AddTwoPar(frmComputers.cmbAsist.Text, frmComputers.PROizV32.Text, "SPR_ASISTEM", frmComputers.cmbAsist)
        End If

        'If Not (RSExists("IBP", "name", frmComputers.CPU(53).Text)) Then
        ''addIBP(frmComputers.CPU(53).Text, frmComputers.PROizV(26).Text)
        'End If


        'If Not (RSExists("SCANER", "name", frmComputers.CPU(53).Text)) Then
        ''addSCANER(frmComputers.CPU(65).Text, frmComputers.PROizV(32).Text)
        'End If

        If Not (RSExists("otv", "name", Trim(frmComputers.cmbResponsible.Text))) Then
            AddOnePar(frmComputers.cmbResponsible.Text, "NAME", "SPR_OTV", frmComputers.cmbResponsible)
        End If


        If Not (RSExists("TIPS", "TIP", Trim(frmComputers.cmbAppointment.Text))) Then
            AddOnePar(frmComputers.cmbAppointment.Text, "TIP", "SPR_TIP", frmComputers.cmbAppointment)
        End If

        If Not (RSExists("Postav", "name", frmComputers.cmbPostav.Text)) Then
            AddOnePar(frmComputers.cmbPostav.Text, "NAME", "SPR_Postav", frmComputers.cmbPostav)
        End If

        If Not (RSExists("BP", "name", frmComputers.cmbBP.Text)) Then
            AddTwoPar(frmComputers.cmbBP.Text, frmComputers.PROizV26.Text, "SPR_BP", frmComputers.cmbBP)
        End If

        If Not (RSExists("CASE", "name", frmComputers.cmbCase.Text)) Then
            AddTwoPar(frmComputers.cmbCase.Text, frmComputers.PROizV25.Text, "SPR_CASE", frmComputers.cmbCase)
        End If

        If Not (RSExists("CREADER", "name", frmComputers.cmbCreader.Text)) Then
            AddTwoPar(frmComputers.cmbCreader.Text, frmComputers.PROizV23.Text, "SPR_CREADER", frmComputers.cmbCreader)
        End If


    End Sub

    Private Sub PRESAVE_TREE(ByVal sBr As ComboBox, ByVal sDp As ComboBox, ByVal sOff As ComboBox)

        If Not (RSExists("FILIAL", "FILIAL", sBr.Text)) Then
            AddOnePar(sBr.Text, "FILIAL", "SPR_FILIAL", sBr)
        End If

        If Not (RSExistsDB(sBr.Text, sDp.Text)) Then
            AddDepartment(sBr.Text, sDp.Text)
        End If

        If Not (RSExistsDBO(sBr.Text, sDp.Text, sOff.Text)) Then
            AddOffice(sBr.Text, sDp.Text, sOff.Text)
        End If
    End Sub

    Public Sub SAVE_INF_BRANCHE()

        Dim counter As Long
        Dim SerD As String
        Dim sSQL As String

        Select Case frmComputers.sPREF

            Case "G"

                SerD = frmComputers.sCOUNT & "F"


            Case "O"

                SerD = frmComputers.sCOUNT & "O_F"

            Case "K"
                SerD = frmComputers.sCOUNT & "K"

        End Select


        Dim rs As ADODB.Recordset
        rs = New ADODB.Recordset

        rs.Open("Select COUNT(*) AS total_number FROM OTD_O where Id_OTD='" & SerD & "'", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        With rs
            counter = .Fields("total_number").Value
        End With

        rs.Close()
        rs = Nothing




        If counter = 0 Then

            sSQL = "SELECT * FROM OTD_O"

        Else

            sSQL = "SELECT * FROM OTD_O WHERE Id_OTD ='" & SerD & "'"

        End If

        rs = New ADODB.Recordset
        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        With rs
            If counter = 0 Then
                .AddNew()
                .Fields("Id_OTD").Value = SerD
            Else

            End If

            .Fields("Phone").Value = frmComputers.txtBRPhone.Text
            .Fields("OTV").Value = frmComputers.txtBRBoss.Text
            .Fields("ADRESS").Value = frmComputers.txtBRAddress.Text
            .Fields("Prim").Value = frmComputers.txtBRMemo.Text

            .Update()
        End With

        rs.Close()
        rs = Nothing

        'Санитарный паспорт

        If Len(frmComputers.txtspplo.Text) = 0 Then Exit Sub

        rs = New ADODB.Recordset
        rs.Open("Select COUNT(*) AS total_number FROM SES_Pass where id_OF='" & SerD & "'", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        With rs
            counter = .Fields("total_number").Value
        End With

        rs.Close()
        rs = Nothing

        If counter = 0 Then

            sSQL = "SELECT * FROM SES_Pass"

        Else

            sSQL = "SELECT * FROM SES_Pass WHERE id_OF ='" & SerD & "'"

        End If

        rs = New ADODB.Recordset
        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        With rs
            If counter = 0 Then
                .AddNew()
                .Fields("id_OF").Value = SerD
            Else

            End If

            .Fields("Ploshad").Value = frmComputers.txtspplo.Text
            .Fields("visota").Value = frmComputers.txtspvis.Text
            .Fields("Pl1Pk").Value = frmComputers.txtspPloOneEVM.Text
            .Fields("ob1Pk").Value = frmComputers.txtspObOneEVM.Text
            .Fields("nalpom").Value = frmComputers.cmbSpRemEVM.Text
            .Fields("vent").Value = frmComputers.cmbSpVent.Text
            .Fields("voda").Value = frmComputers.cmbSpWater.Text
            .Fields("kanal").Value = frmComputers.cmbSpTeplo.Text
            .Fields("teplo").Value = frmComputers.cmbSpKanal.Text
            .Fields("otdelka").Value = frmComputers.txtSpWall.Text
            .Fields("mebel").Value = frmComputers.txtSpMebel.Text

            .Update()
        End With

        rs.Close()
        rs = Nothing


    End Sub

    Public Sub SAVE_DRAG_DROP(ByVal sID As String, ByVal sBRANCHE As String, ByVal sDEPARTMENT As String, ByVal sOFFICE As String, ByVal sNAME As String)

        If frmComputers.EDT = True Then
            Call DVIG_TEHN(sBRANCHE, sDEPARTMENT, sOFFICE, sNAME)

            If DV = True Then

            Else
                If Sav = False Then
                    MsgBox("Отмена перемещения", MsgBoxStyle.Exclamation, ProGramName)
                    Exit Sub
                End If

            End If

        End If

        Dim sSQL As String

        sSQL = "SELECT * FROM kompy where id=" & sID

        Dim rs As ADODB.Recordset
        rs = New ADODB.Recordset
        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        With rs
            .Fields("FILIAL").Value = sBRANCHE
            .Fields("mesto").Value = sDEPARTMENT
            .Fields("kabn").Value = sOFFICE
            .Fields("PCL").Value = 0
            .Update()
        End With

        rs.Close()
        rs = Nothing


        sSQL = "SELECT count(*) as t_n FROM kompy where PCL=" & sID
        rs = New ADODB.Recordset
        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        Dim sCON As String

        With rs
            sCON = .Fields("t_n").Value
        End With
        rs.Close()
        rs = Nothing

        If sCON > 0 Then

            rs = New ADODB.Recordset
            rs.Open("update kompy set FILIAL='" & sBRANCHE & "', mesto='" & sDEPARTMENT & "', kabn='" & sOFFICE & "' WHERE PCL=" & sID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

            'rs.Close()
            rs = Nothing

        End If



    End Sub

    Public Sub under_prn(Optional ByVal sSID As Integer = 0)
        If sSID = 0 Then sSID = frmComputers.sCOUNT

        On Error Resume Next

        Dim sCount As Integer
        Dim rs As ADODB.Recordset
        Dim rs1 As ADODB.Recordset


        Dim sSQL As String
        Dim tId As Integer


        rs = New ADODB.Recordset
        rs.Open("Select count(*) as t_n from kompy where PCL=" & sSID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        With rs

            sCount = .Fields("t_n").Value

        End With
        rs.Close()
        rs = Nothing

        If sCount = 0 Then Exit Sub


        Dim objIniFile As New IniFile(PrPath & "base.ini")

        Dim sRAZDEL As String
        sRAZDEL = objIniFile.GetString("General", "RAZDEL", "0")

        Select Case sRAZDEL

            Case 0 'все


                'Мониторы
                sSQL = "SELECT count(*) as t_n FROM kompy where PCL=" & sSID & " and tiptehn = 'MONITOR'"

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs

                    sCount = .Fields("t_n").Value

                End With
                rs.Close()
                rs = Nothing

                sSQL = "SELECT * FROM kompy where PCL=" & sSID & " and tiptehn = 'MONITOR'"

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    .MoveFirst()
                    Do While Not .EOF

                        tId = .Fields("id").Value

                        rs1 = New ADODB.Recordset
                        rs1.Open("Select * from kompy where id=" & sSID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        Select Case sCount

                            Case 1

                                With rs1
                                    rs1.Fields("MONITOR_NAME").Value = rs.Fields("MONITOR_NAME").Value
                                    rs1.Fields("MONITOR_DUM").Value = rs.Fields("MONITOR_DUM").Value
                                    rs1.Fields("MONITOR_SN").Value = rs.Fields("MONITOR_SN").Value
                                    rs1.Fields("MONITOR_PROIZV").Value = rs.Fields("MONITOR_PROIZV").Value
                                    rs1.Fields("INV_NO_MONITOR").Value = rs.Fields("INV_NO_MONITOR").Value
                                    .Update()
                                End With

                            Case 2

                                With rs1
                                    rs1.Fields("MONITOR_NAME2").Value = rs.Fields("MONITOR_NAME").Value
                                    rs1.Fields("MONITOR_DUM2").Value = rs.Fields("MONITOR_DUM").Value
                                    rs1.Fields("MONITOR_SN2").Value = rs.Fields("MONITOR_SN").Value
                                    rs1.Fields("MONITOR_PROIZV2").Value = rs.Fields("MONITOR_PROIZV").Value
                                    rs1.Fields("INV_NO_MONITOR2").Value = rs.Fields("INV_NO_MONITOR").Value
                                    .Update()
                                End With

                        End Select


                        rs1.Close()
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Remont SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Zametki SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from Garantia_sis where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from kompy where id=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing


                        .MoveNext()
                    Loop
                End With
                rs.Close()
                rs = Nothing

                'Принтеры
                sSQL = "SELECT count(*) as t_n FROM kompy where PCL=" & sSID & " and tiptehn = 'Printer'"

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs

                    sCount = .Fields("t_n").Value

                End With
                rs.Close()
                rs = Nothing

                sSQL = "SELECT * FROM kompy where PCL=" & sSID & " and tiptehn = 'Printer'"

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    .MoveFirst()
                    Do While Not .EOF

                        tId = .Fields("id").Value

                        rs1 = New ADODB.Recordset
                        rs1.Open("Select * from kompy where id=" & sSID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        Select Case sCount

                            Case 1

                                With rs1
                                    rs1.Fields("PRINTER_NAME_1").Value = rs.Fields("PRINTER_NAME_1").Value
                                    rs1.Fields("PRINTER_SN_1").Value = rs.Fields("PRINTER_SN_1").Value
                                    rs1.Fields("PRINTER_PROIZV_1").Value = rs.Fields("PRINTER_PROIZV_1").Value
                                    rs1.Fields("INV_NO_PRINTER").Value = rs.Fields("INV_NO_PRINTER").Value
                                    rs1.Fields("port_1").Value = rs.Fields("port_2").Value
                                    .Update()
                                End With

                            Case 2

                                With rs1
                                    rs1.Fields("PRINTER_NAME_2").Value = rs.Fields("PRINTER_NAME_2").Value
                                    rs1.Fields("PRINTER_SN_2").Value = rs.Fields("PRINTER_SN_2").Value
                                    rs1.Fields("PRINTER_PROIZV_2").Value = rs.Fields("PRINTER_PROIZV_2").Value
                                    rs1.Fields("INV_NO_PRINTER").Value = rs.Fields("INV_NO_PRINTER").Value
                                    rs1.Fields("port_2").Value = rs.Fields("port_2").Value
                                    .Update()
                                End With

                            Case 3

                                With rs1
                                    rs1.Fields("PRINTER_NAME_3").Value = rs.Fields("PRINTER_NAME_3").Value
                                    rs1.Fields("PRINTER_SN_3").Value = rs.Fields("PRINTER_SN_3").Value
                                    rs1.Fields("PRINTER_PROIZV_3").Value = rs.Fields("PRINTER_PROIZV_31").Value
                                    rs1.Fields("INV_NO_PRINTER").Value = rs.Fields("INV_NO_PRINTER").Value
                                    rs1.Fields("port_3").Value = rs.Fields("port_2").Value
                                    .Update()
                                End With

                        End Select


                        rs1.Close()
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Remont SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Zametki SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from Garantia_sis where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from kompy where id=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing


                        .MoveNext()
                    Loop
                End With
                rs.Close()
                rs = Nothing

                'ИБП
                sSQL = "SELECT * FROM kompy where PCL=" & sSID & " and tiptehn = 'IBP'"

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    .MoveFirst()
                    Do While Not .EOF

                        tId = .Fields("id").Value

                        rs1 = New ADODB.Recordset
                        rs1.Open("Select * from kompy where id=" & sSID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        With rs1
                            rs1.Fields("IBP_NAME").Value = rs.Fields("PRINTER_NAME_1").Value
                            rs1.Fields("IBP_SN").Value = rs.Fields("PRINTER_SN_1").Value
                            rs1.Fields("IBP_PROIZV").Value = rs.Fields("PRINTER_PROIZV_1").Value
                            rs1.Fields("INV_NO_IBP").Value = rs.Fields("INV_NO_PRINTER").Value

                            .Update()
                        End With

                        rs1.Close()
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Remont SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Zametki SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from Garantia_sis where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from kompy where id=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing


                        .MoveNext()
                    Loop
                End With
                rs.Close()
                rs = Nothing

                'Клавиатуры мыши
                sSQL = "SELECT * FROM kompy where PCL=" & sSID & " and tiptehn = 'KEYB'"

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    .MoveFirst()
                    Do While Not .EOF

                        tId = .Fields("id").Value

                        rs1 = New ADODB.Recordset
                        rs1.Open("Select * from kompy where id=" & sSID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        With rs1
                            rs1.Fields("KEYBOARD_NAME").Value = rs.Fields("PRINTER_NAME_1").Value
                            rs1.Fields("KEYBOARD_SN").Value = rs.Fields("PRINTER_SN_1").Value
                            rs1.Fields("KEYBOARD_PROIZV").Value = rs.Fields("PRINTER_PROIZV_1").Value
                            .Update()
                        End With

                        rs1.Close()
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Remont SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Zametki SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from Garantia_sis where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from kompy where id=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing


                        .MoveNext()
                    Loop
                End With
                rs.Close()
                rs = Nothing


                sSQL = "SELECT * FROM kompy where PCL=" & sSID & " and tiptehn = 'MOUSE'"

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    .MoveFirst()
                    Do While Not .EOF

                        tId = .Fields("id").Value

                        rs1 = New ADODB.Recordset
                        rs1.Open("Select * from kompy where id=" & sSID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        With rs1
                            rs1.Fields("MOUSE_NAME").Value = rs.Fields("PRINTER_NAME_1").Value
                            rs1.Fields("MOUSE_SN").Value = rs.Fields("PRINTER_SN_1").Value
                            rs1.Fields("MOUSE_PROIZV").Value = rs.Fields("PRINTER_PROIZV_1").Value
                            .Update()
                        End With

                        rs1.Close()
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Remont SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Zametki SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from Garantia_sis where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from kompy where id=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        .MoveNext()
                    Loop
                End With
                rs.Close()
                rs = Nothing

                'Сетевые фильтры

                sSQL = "SELECT * FROM kompy where PCL=" & sSID & " and tiptehn = 'FS'"

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    .MoveFirst()
                    Do While Not .EOF

                        tId = .Fields("id").Value

                        rs1 = New ADODB.Recordset
                        rs1.Open("Select * from kompy where id=" & sSID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        With rs1
                            rs1.Fields("FILTR_NAME").Value = rs.Fields("PRINTER_NAME_1").Value
                            rs1.Fields("FILTR_SN").Value = rs.Fields("PRINTER_SN_1").Value
                            rs1.Fields("FILTR_PROIZV").Value = rs.Fields("PRINTER_PROIZV_1").Value
                            .Update()
                        End With

                        rs1.Close()
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Remont SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Zametki SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from Garantia_sis where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from kompy where id=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        .MoveNext()
                    Loop
                End With
                rs.Close()
                rs = Nothing




            Case 1 'Мониторы

                sSQL = "SELECT count(*) as t_n FROM kompy where PCL=" & sSID & " and tiptehn = 'MONITOR'"

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs

                    sCount = .Fields("t_n").Value

                End With
                rs.Close()
                rs = Nothing

                sSQL = "SELECT * FROM kompy where PCL=" & sSID & " and tiptehn = 'MONITOR'"

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    .MoveFirst()
                    Do While Not .EOF

                        tId = .Fields("id").Value

                        rs1 = New ADODB.Recordset
                        rs1.Open("Select * from kompy where id=" & sSID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        Select Case sCount

                            Case 1

                                With rs1
                                    rs1.Fields("MONITOR_NAME").Value = rs.Fields("MONITOR_NAME").Value
                                    rs1.Fields("MONITOR_DUM").Value = rs.Fields("MONITOR_DUM").Value
                                    rs1.Fields("MONITOR_SN").Value = rs.Fields("MONITOR_SN").Value
                                    rs1.Fields("MONITOR_PROIZV").Value = rs.Fields("MONITOR_PROIZV").Value
                                    rs1.Fields("INV_NO_MONITOR").Value = rs.Fields("INV_NO_MONITOR").Value
                                    .Update()
                                End With

                            Case 2

                                With rs1
                                    rs1.Fields("MONITOR_NAME2").Value = rs.Fields("MONITOR_NAME").Value
                                    rs1.Fields("MONITOR_DUM2").Value = rs.Fields("MONITOR_DUM").Value
                                    rs1.Fields("MONITOR_SN2").Value = rs.Fields("MONITOR_SN").Value
                                    rs1.Fields("MONITOR_PROIZV2").Value = rs.Fields("MONITOR_PROIZV").Value
                                    rs1.Fields("INV_NO_MONITOR2").Value = rs.Fields("INV_NO_MONITOR").Value
                                    .Update()
                                End With

                        End Select


                        rs1.Close()
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Remont SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Zametki SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from Garantia_sis where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from kompy where id=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing


                        .MoveNext()
                    Loop
                End With
                rs.Close()
                rs = Nothing

            Case 2 'Принтеры

                sSQL = "SELECT count(*) as t_n FROM kompy where PCL=" & sSID & " and tiptehn = 'Printer'"

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs

                    sCount = .Fields("t_n").Value

                End With
                rs.Close()
                rs = Nothing

                sSQL = "SELECT * FROM kompy where PCL=" & sSID & " and tiptehn = 'Printer'"

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    .MoveFirst()
                    Do While Not .EOF

                        tId = .Fields("id").Value

                        rs1 = New ADODB.Recordset
                        rs1.Open("Select * from kompy where id=" & sSID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        Select Case sCount

                            Case 1

                                With rs1
                                    rs1.Fields("PRINTER_NAME_1").Value = rs.Fields("PRINTER_NAME_1").Value
                                    rs1.Fields("PRINTER_SN_1").Value = rs.Fields("PRINTER_SN_1").Value
                                    rs1.Fields("PRINTER_PROIZV_1").Value = rs.Fields("PRINTER_PROIZV_1").Value
                                    rs1.Fields("INV_NO_PRINTER").Value = rs.Fields("INV_NO_PRINTER").Value
                                    rs1.Fields("port_1").Value = rs.Fields("port_2").Value
                                    .Update()
                                End With

                            Case 2

                                With rs1
                                    rs1.Fields("PRINTER_NAME_2").Value = rs.Fields("PRINTER_NAME_2").Value
                                    rs1.Fields("PRINTER_SN_2").Value = rs.Fields("PRINTER_SN_2").Value
                                    rs1.Fields("PRINTER_PROIZV_2").Value = rs.Fields("PRINTER_PROIZV_2").Value
                                    rs1.Fields("INV_NO_PRINTER").Value = rs.Fields("INV_NO_PRINTER").Value
                                    rs1.Fields("port_2").Value = rs.Fields("port_2").Value
                                    .Update()
                                End With

                            Case 3

                                With rs1
                                    rs1.Fields("PRINTER_NAME_3").Value = rs.Fields("PRINTER_NAME_3").Value
                                    rs1.Fields("PRINTER_SN_3").Value = rs.Fields("PRINTER_SN_3").Value
                                    rs1.Fields("PRINTER_PROIZV_3").Value = rs.Fields("PRINTER_PROIZV_31").Value
                                    rs1.Fields("INV_NO_PRINTER").Value = rs.Fields("INV_NO_PRINTER").Value
                                    rs1.Fields("port_3").Value = rs.Fields("port_2").Value
                                    .Update()
                                End With

                        End Select


                        rs1.Close()
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Remont SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Zametki SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from Garantia_sis where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from kompy where id=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing


                        .MoveNext()
                    Loop
                End With
                rs.Close()
                rs = Nothing



            Case 3 'ИБП


                sSQL = "SELECT * FROM kompy where PCL=" & sSID & " and tiptehn = 'IBP'"

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    .MoveFirst()
                    Do While Not .EOF

                        tId = .Fields("id").Value

                        rs1 = New ADODB.Recordset
                        rs1.Open("Select * from kompy where id=" & sSID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        With rs1
                            rs1.Fields("IBP_NAME").Value = rs.Fields("PRINTER_NAME_1").Value
                            rs1.Fields("IBP_SN").Value = rs.Fields("PRINTER_SN_1").Value
                            rs1.Fields("IBP_PROIZV").Value = rs.Fields("PRINTER_PROIZV_1").Value
                            rs1.Fields("INV_NO_IBP").Value = rs.Fields("INV_NO_PRINTER").Value

                            .Update()
                        End With

                        rs1.Close()
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Remont SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Zametki SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from Garantia_sis where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from kompy where id=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing


                        .MoveNext()
                    Loop
                End With
                rs.Close()
                rs = Nothing
            Case 4 'Клавиатуры мыши

                sSQL = "SELECT * FROM kompy where PCL=" & sSID & " and tiptehn = 'KEYB'"

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    .MoveFirst()
                    Do While Not .EOF

                        tId = .Fields("id").Value

                        rs1 = New ADODB.Recordset
                        rs1.Open("Select * from kompy where id=" & sSID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        With rs1
                            rs1.Fields("KEYBOARD_NAME").Value = rs.Fields("PRINTER_NAME_1").Value
                            rs1.Fields("KEYBOARD_SN").Value = rs.Fields("PRINTER_SN_1").Value
                            rs1.Fields("KEYBOARD_PROIZV").Value = rs.Fields("PRINTER_PROIZV_1").Value
                            .Update()
                        End With

                        rs1.Close()
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Remont SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Zametki SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from Garantia_sis where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from kompy where id=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing


                        .MoveNext()
                    Loop
                End With
                rs.Close()
                rs = Nothing


                sSQL = "SELECT * FROM kompy where PCL=" & sSID & " and tiptehn = 'MOUSE'"

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    .MoveFirst()
                    Do While Not .EOF

                        tId = .Fields("id").Value

                        rs1 = New ADODB.Recordset
                        rs1.Open("Select * from kompy where id=" & sSID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        With rs1
                            rs1.Fields("MOUSE_NAME").Value = rs.Fields("PRINTER_NAME_1").Value
                            rs1.Fields("MOUSE_SN").Value = rs.Fields("PRINTER_SN_1").Value
                            rs1.Fields("MOUSE_PROIZV").Value = rs.Fields("PRINTER_PROIZV_1").Value
                            .Update()
                        End With

                        rs1.Close()
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Remont SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Zametki SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from Garantia_sis where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from kompy where id=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        .MoveNext()
                    Loop
                End With
                rs.Close()
                rs = Nothing


            Case 5 'Сетевые фильтры

                sSQL = "SELECT * FROM kompy where PCL=" & sSID & " and tiptehn = 'FS'"

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    .MoveFirst()
                    Do While Not .EOF

                        tId = .Fields("id").Value

                        rs1 = New ADODB.Recordset
                        rs1.Open("Select * from kompy where id=" & sSID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        With rs1
                            rs1.Fields("FILTR_NAME").Value = rs.Fields("PRINTER_NAME_1").Value
                            rs1.Fields("FILTR_SN").Value = rs.Fields("PRINTER_SN_1").Value
                            rs1.Fields("FILTR_PROIZV").Value = rs.Fields("PRINTER_PROIZV_1").Value
                            .Update()
                        End With

                        rs1.Close()
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Remont SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("UPDATE Zametki SET Id_Comp=" & sSID & " where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from Garantia_sis where Id_Comp=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        rs1 = New ADODB.Recordset
                        rs1.Open("Delete from kompy where id=" & tId, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                        rs1 = Nothing

                        .MoveNext()
                    Loop
                End With
                rs.Close()
                rs = Nothing

        End Select


        If MRZD = True Then Exit Sub

        RefFilTree(frmComputers.lstGroups)

    End Sub


    Public Sub Re_PRN(Optional ByVal sSID As Integer = 0)
        On Error GoTo err_
        Dim sADD As Boolean

        If sSID = 0 Then sSID = frmComputers.sCOUNT
        Dim rsdb As ADODB.Recordset
        rsdb = New ADODB.Recordset
        rsdb.Open("Select * from kompy where id=" & sSID & " and tiptehn ='PC'", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)


        With rsdb

            Dim sTEMP0, sTEMP1, sTEMP3 As String


            Dim objIniFile As New IniFile(PrPath & "base.ini")

            sTEMP3 = objIniFile.GetString("General", "RAZDEL", "0")


            If sTEMP3 = 0 Or sTEMP3 = 2 Then

                sTEMP0 = rsdb.Fields("PRINTER_NAME_1").Value
                sTEMP1 = rsdb.Fields("PORT_1").Value

                sADD = False

                If Left(sTEMP0, 2) <> "\\" And Left(sTEMP1, 2) <> "IP" And Len(sTEMP0) <> 0 And sTEMP0 <> "Microsoft Office Document Image Writer" Then


                    If Not RSPRNExt(sTEMP0, sSID) Then
                        sADD = True
                        Call SaveActivityToLogDB("Добавление принтера в результате разделения " & sTEMP0)

                        Dim rs As ADODB.Recordset
                        Dim sSQL As String

                        sSQL = "SELECT * FROM kompy"

                        rs = New ADODB.Recordset
                        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        With rs
                            .AddNew()

                            .Fields("date").Value = Date.Today
                            .Fields("TIPtehn").Value = "Printer"

                            If Not IsDBNull(rsdb.Fields("PRINTER_NAME_1").Value) Then .Fields("PRINTER_NAME_1").Value = rsdb.Fields("PRINTER_NAME_1").Value
                            If Not IsDBNull(rsdb.Fields("PRINTER_SN_1").Value) Then .Fields("PRINTER_SN_1").Value = rsdb.Fields("PRINTER_SN_1").Value
                            If Not IsDBNull(rsdb.Fields("PRINTER_PROIZV_1").Value) Then .Fields("PRINTER_PROIZV_1").Value = rsdb.Fields("PRINTER_PROIZV_1").Value
                            If Not IsDBNull(rsdb.Fields("INV_NO_PRINTER").Value) Then .Fields("INV_NO_PRINTER").Value = rsdb.Fields("INV_NO_PRINTER").Value


                            .Fields("port_1").Value = ""
                            .Fields("FILIAL").Value = sBranch
                            .Fields("mesto").Value = sDepartment
                            .Fields("kabn").Value = sOffice
                            If Not IsDBNull(rsdb.Fields("OTvetstvennyj").Value) Then .Fields("OTvetstvennyj").Value = rsdb.Fields("OTvetstvennyj").Value
                            If Not IsDBNull(rsdb.Fields("TELEPHONE").Value) Then .Fields("TELEPHONE").Value = rsdb.Fields("TELEPHONE").Value
                            .Fields("NET_IP_1").Value = ""
                            .Fields("NET_MAC_1").Value = ""
                            .Fields("OS").Value = ""
                            .Fields("NET_NAME").Value = rsdb.Fields("PRINTER_NAME_1").Value
                            .Fields("PSEVDONIM").Value = rsdb.Fields("PRINTER_NAME_1").Value
                            .Fields("PCL").Value = sSID

                            .Fields("port_2").Value = rsdb.Fields("port_1").Value
                            .Fields("SFAktNo").Value = 0
                            .Fields("CenaRub").Value = 0
                            .Fields("StoimRub").Value = 0
                            .Fields("Zaiavk").Value = 0
                            .Fields("DataVVoda").Value = rsdb.Fields("DataVVoda").Value
                            .Fields("dataSF").Value = rsdb.Fields("dataSF").Value
                            .Fields("Spisan").Value = 0
                            .Fields("Balans").Value = 0

                            .Update()
                        End With

                        rs.Close()
                        rs = Nothing


                        sSQL = "SELECT * FROM kompy where id=" & sSID

                        rs = New ADODB.Recordset
                        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        With rs
                            .Fields("PRINTER_NAME_1").Value = ""
                            .Fields("PRINTER_SN_1").Value = ""
                            .Fields("PRINTER_PROIZV_1").Value = ""
                            .Fields("INV_NO_PRINTER").Value = ""
                            .Fields("port_1").Value = ""
                            .Update()
                        End With

                        rs.Close()
                        rs = Nothing



                        Dim rsBK As ADODB.Recordset
                        rsBK = New ADODB.Recordset
                        rsBK.Open("SELECT id FROM kompy WHERE NET_NAME='" & sTEMP0 & "' and MESTO='" & sDepartment & "' and FILIAL='" & sBranch & "'  and kabn='" & sOffice & "' and PCL=" & sSID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        Dim sPRN As String
                        With rsBK

                            sPRN = .Fields("ID").Value

                        End With
                        rsBK.Close()
                        rsBK = Nothing

                        objIniFile.WriteString("general", "DK", sPRN)
                        objIniFile.WriteString("general", "Default", 0)

                    End If

                End If

                sTEMP0 = rsdb.Fields("PRINTER_NAME_2").Value
                sTEMP1 = rsdb.Fields("PORT_2").Value


                If Left(sTEMP0, 2) <> "\\" And Left(sTEMP1, 2) <> "IP" And Len(sTEMP0) <> 0 And sTEMP0 <> "Microsoft Office Document Image Writer" Then


                    If Not RSPRNExt(sTEMP0, sSID) Then

                        sADD = True
                        Call SaveActivityToLogDB("Добавление принтера в результате разделения " & sTEMP0)

                        Dim rs As ADODB.Recordset
                        Dim sSQL As String

                        sSQL = "SELECT * FROM kompy"

                        rs = New ADODB.Recordset
                        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)


                        With rs
                            .AddNew()

                            .Fields("date").Value = Date.Today
                            .Fields("TIPtehn").Value = "Printer"

                            If Not IsDBNull(rsdb.Fields("PRINTER_NAME_2").Value) Then .Fields("PRINTER_NAME_1").Value = rsdb.Fields("PRINTER_NAME_2").Value
                            If Not IsDBNull(rsdb.Fields("PRINTER_SN_2").Value) Then .Fields("PRINTER_SN_1").Value = rsdb.Fields("PRINTER_SN_2").Value
                            If Not IsDBNull(rsdb.Fields("PRINTER_PROIZV_2").Value) Then .Fields("PRINTER_PROIZV_1").Value = rsdb.Fields("PRINTER_PROIZV_2").Value
                            If Not IsDBNull(rsdb.Fields("INV_NO_PRINTER").Value) Then .Fields("INV_NO_PRINTER").Value = rsdb.Fields("INV_NO_PRINTER").Value


                            .Fields("port_1").Value = ""
                            .Fields("FILIAL").Value = sBranch
                            .Fields("mesto").Value = sDepartment
                            .Fields("kabn").Value = sOffice
                            If Not IsDBNull(rsdb.Fields("OTvetstvennyj").Value) Then .Fields("OTvetstvennyj").Value = rsdb.Fields("OTvetstvennyj").Value
                            If Not IsDBNull(rsdb.Fields("TELEPHONE").Value) Then .Fields("TELEPHONE").Value = rsdb.Fields("TELEPHONE").Value
                            .Fields("NET_IP_1").Value = ""
                            .Fields("NET_MAC_1").Value = ""
                            .Fields("OS").Value = ""
                            .Fields("NET_NAME").Value = rsdb.Fields("PRINTER_NAME_2").Value
                            .Fields("PSEVDONIM").Value = rsdb.Fields("PRINTER_NAME_2").Value
                            .Fields("PCL").Value = sSID

                            .Fields("port_2").Value = rsdb.Fields("port_2").Value

                            .Fields("SFAktNo").Value = 0
                            .Fields("CenaRub").Value = 0
                            .Fields("StoimRub").Value = 0

                            .Fields("Zaiavk").Value = 0

                            .Fields("DataVVoda").Value = rsdb.Fields("DataVVoda").Value
                            .Fields("dataSF").Value = rsdb.Fields("dataSF").Value

                            .Fields("Spisan").Value = 0
                            .Fields("Balans").Value = 0


                            .Update()
                        End With

                        rs.Close()
                        rs = Nothing


                        sSQL = "SELECT * FROM kompy where id=" & sSID

                        rs = New ADODB.Recordset
                        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        With rs
                            .Fields("PRINTER_NAME_2").Value = ""
                            .Fields("PRINTER_SN_2").Value = ""
                            .Fields("PRINTER_PROIZV_2").Value = ""
                            .Fields("INV_NO_PRINTER").Value = ""
                            .Fields("port_2").Value = ""
                            .Update()
                        End With

                        rs.Close()
                        rs = Nothing

                        Dim rsBK As ADODB.Recordset
                        rsBK = New ADODB.Recordset
                        rsBK.Open("SELECT id FROM kompy WHERE NET_NAME='" & sTEMP0 & "' and MESTO='" & sDepartment & "' and FILIAL='" & sBranch & "'  and kabn='" & sOffice & "' and PCL=" & sSID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        Dim sPRN As String
                        With rsBK

                            sPRN = .Fields("ID").Value

                        End With
                        rsBK.Close()
                        rsBK = Nothing


                        objIniFile.WriteString("general", "DK", sPRN)
                        objIniFile.WriteString("general", "Default", 0)

                    End If
                End If


                sTEMP0 = rsdb.Fields("PRINTER_NAME_3").Value
                sTEMP1 = rsdb.Fields("PORT_3").Value


                If Left(sTEMP0, 2) <> "\\" And Left(sTEMP1, 2) <> "IP" And Len(sTEMP0) <> 0 And sTEMP0 <> "Microsoft Office Document Image Writer" Then

                    If Not RSPRNExt(sTEMP0, sSID) Then
                        sADD = True

                        Call SaveActivityToLogDB("Добавление принтера в результате разделения " & sTEMP0)

                        Dim rs As ADODB.Recordset
                        Dim sSQL As String

                        sSQL = "SELECT * FROM kompy"

                        rs = New ADODB.Recordset
                        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)


                        With rs

                            .AddNew()
                            .Fields("date").Value = Date.Today
                            .Fields("TIPtehn").Value = "Printer"

                            If Not IsDBNull(rsdb.Fields("PRINTER_NAME_3").Value) Then .Fields("PRINTER_NAME_1").Value = rsdb.Fields("PRINTER_NAME_3").Value
                            If Not IsDBNull(rsdb.Fields("PRINTER_SN_3").Value) Then .Fields("PRINTER_SN_1").Value = rsdb.Fields("PRINTER_SN_3").Value
                            If Not IsDBNull(rsdb.Fields("PRINTER_PROIZV_3").Value) Then .Fields("PRINTER_PROIZV_1").Value = rsdb.Fields("PRINTER_PROIZV_3").Value
                            If Not IsDBNull(rsdb.Fields("INV_NO_PRINTER").Value) Then .Fields("INV_NO_PRINTER").Value = rsdb.Fields("INV_NO_PRINTER").Value

                            .Fields("port_1").Value = ""
                            .Fields("FILIAL").Value = sBranch
                            .Fields("mesto").Value = sDepartment
                            .Fields("kabn").Value = sOffice

                            If Not IsDBNull(rsdb.Fields("OTvetstvennyj").Value) Then .Fields("OTvetstvennyj").Value = rsdb.Fields("OTvetstvennyj").Value
                            If Not IsDBNull(rsdb.Fields("TELEPHONE").Value) Then .Fields("TELEPHONE").Value = rsdb.Fields("TELEPHONE").Value

                            .Fields("NET_IP_1").Value = ""
                            .Fields("NET_MAC_1").Value = ""
                            .Fields("OS").Value = ""
                            .Fields("NET_NAME").Value = rsdb.Fields("PRINTER_NAME_3").Value
                            .Fields("PSEVDONIM").Value = rsdb.Fields("PRINTER_NAME_3").Value
                            .Fields("PCL").Value = sSID

                            .Fields("port_2").Value = rsdb.Fields("port_3").Value

                            .Fields("SFAktNo").Value = 0
                            .Fields("CenaRub").Value = 0
                            .Fields("StoimRub").Value = 0

                            .Fields("Zaiavk").Value = 0

                            .Fields("DataVVoda").Value = rsdb.Fields("DataVVoda").Value
                            .Fields("dataSF").Value = rsdb.Fields("dataSF").Value

                            .Fields("Spisan").Value = 0
                            .Fields("Balans").Value = 0


                            .Update()
                        End With

                        rs.Close()
                        rs = Nothing


                        sSQL = "SELECT * FROM kompy where id=" & sSID

                        rs = New ADODB.Recordset
                        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        With rs
                            .Fields("PRINTER_NAME_3").Value = ""
                            .Fields("PRINTER_SN_3").Value = ""
                            .Fields("PRINTER_PROIZV_3").Value = ""
                            .Fields("INV_NO_PRINTER").Value = ""
                            .Fields("port_3").Value = ""
                            .Update()
                        End With

                        rs.Close()
                        rs = Nothing


                        Dim rsBK As ADODB.Recordset
                        rsBK = New ADODB.Recordset
                        rsBK.Open("SELECT id FROM kompy WHERE NET_NAME='" & sTEMP0 & "' and MESTO='" & sDepartment & "' and FILIAL='" & sBranch & "'  and kabn='" & sOffice & "' and PCL=" & sSID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        Dim sPRN As String
                        With rsBK

                            sPRN = .Fields("ID").Value

                        End With
                        rsBK.Close()
                        rsBK = Nothing

                        objIniFile.WriteString("general", "DK", sPRN)
                        objIniFile.WriteString("general", "Default", 0)

                    End If
                End If

            End If

            If sTEMP3 = 0 Or sTEMP3 = 1 Then
                'МОНИТОР
                sTEMP0 = rsdb.Fields("MONITOR_NAME").Value


                If Left(sTEMP0, 2) <> "Мо" And Len(sTEMP0) <> 0 Then

                    If Not RSPRNExt(sTEMP0, sSID) Then
                        sADD = True
                        Call SaveActivityToLogDB("Добавление монитора в результате разделения " & sTEMP0)
                        Dim rs As ADODB.Recordset
                        Dim sSQL As String

                        sSQL = "SELECT * FROM kompy"

                        rs = New ADODB.Recordset
                        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)


                        With rs

                            .AddNew()

                            .Fields("MONITOR_NAME").Value = rsdb.Fields("MONITOR_NAME").Value
                            .Fields("MONITOR_DUM").Value = rsdb.Fields("MONITOR_DUM").Value
                            .Fields("MONITOR_SN").Value = rsdb.Fields("MONITOR_SN").Value
                            .Fields("MONITOR_PROIZV").Value = rsdb.Fields("MONITOR_PROIZV").Value

                            .Fields("port_1").Value = ""



                            If Not IsDBNull(rsdb.Fields("INV_NO_MONITOR").Value) Then .Fields("INV_NO_MONITOR").Value = rsdb.Fields("INV_NO_MONITOR").Value

                            If Not IsDBNull(rsdb.Fields("OTvetstvennyj").Value) Then .Fields("OTvetstvennyj").Value = rsdb.Fields("OTvetstvennyj").Value
                            If Not IsDBNull(rsdb.Fields("TELEPHONE").Value) Then .Fields("TELEPHONE").Value = rsdb.Fields("TELEPHONE").Value

                            If Not IsDBNull(rsdb.Fields("MONITOR_NAME").Value) Then .Fields("NET_NAME").Value = rsdb.Fields("MONITOR_NAME").Value
                            If Not IsDBNull(rsdb.Fields("MONITOR_NAME").Value) Then .Fields("PSEVDONIM").Value = rsdb.Fields("MONITOR_NAME").Value



                            .Fields("PCL").Value = sSID
                            .Fields("date").Value = Date.Today
                            .Fields("TIPtehn").Value = "MONITOR"

                            .Fields("FILIAL").Value = sBranch
                            .Fields("mesto").Value = sDepartment
                            .Fields("kabn").Value = sOffice

                            .Fields("SFAktNo").Value = 0
                            .Fields("CenaRub").Value = 0
                            .Fields("StoimRub").Value = 0

                            .Fields("Zaiavk").Value = 0

                            .Fields("DataVVoda").Value = rsdb.Fields("DataVVoda").Value
                            .Fields("dataSF").Value = rsdb.Fields("dataSF").Value

                            .Fields("Spisan").Value = 0
                            .Fields("Balans").Value = 0

                            .Update()
                        End With

                        rs.Close()
                        rs = Nothing


                        sSQL = "SELECT * FROM kompy where id=" & sSID

                        rs = New ADODB.Recordset
                        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        With rs
                            .Fields("MONITOR_NAME").Value = ""
                            .Fields("MONITOR_DUM").Value = ""
                            .Fields("MONITOR_SN").Value = ""
                            .Fields("MONITOR_PROIZV").Value = ""
                            .Fields("INV_NO_MONITOR").Value = ""
                            .Update()
                        End With

                        rs.Close()
                        rs = Nothing

                        On Error Resume Next

                        Dim rsBK As ADODB.Recordset
                        rsBK = New ADODB.Recordset
                        rsBK.Open("SELECT id FROM kompy WHERE MONITOR_NAME='" & sTEMP0 & "' and MESTO='" & sDepartment & "' and FILIAL='" & sBranch & "' and kabn='" & sOffice & "' and PCL=" & sSID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        Dim sPRN As String
                        With rsBK

                            sPRN = .Fields("ID").Value

                        End With
                        rsBK.Close()
                        rsBK = Nothing


                        objIniFile.WriteString("general", "DK", sPRN)
                        objIniFile.WriteString("general", "Default", 0)

                    End If
                End If

            End If


            If sTEMP3 = 0 Or sTEMP3 = 3 Then
                'ИБП
                sTEMP0 = rsdb.Fields("IBP_NAME").Value

                If Left(sTEMP0, 2) <> "Мо" And Len(sTEMP0) <> 0 Then

                    If Not RSPRNExt(sTEMP0, sSID) Then

                        sADD = True
                        Call SaveActivityToLogDB("Добавление ИБП в результате разделения " & sTEMP0)
                        Dim rs As ADODB.Recordset
                        Dim sSQL As String


                        If Not RSExists("SPR_IBP", "name", Trim(sTEMP0)) Then
                            AddTwoPar(sTEMP0, rsdb.Fields("IBP_PROIZV").Value, "SPR_IBP", frmComputers.cmbOTH)
                        End If


                        sSQL = "SELECT * FROM kompy"

                        rs = New ADODB.Recordset
                        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)


                        With rs

                            .AddNew()

                            .Fields("PRINTER_NAME_1").Value = sTEMP0

                            If Not IsDBNull(rsdb.Fields("IBP_SN").Value) Then .Fields("PRINTER_SN_1").Value = rsdb.Fields("IBP_SN").Value
                            If Not IsDBNull(rsdb.Fields("PRINTER_PROIZV_1").Value) Then .Fields("IBP_PROIZV").Value = rsdb.Fields("PRINTER_PROIZV_1").Value

                            .Fields("port_1").Value = ""

                            If Not IsDBNull(rsdb.Fields("OTvetstvennyj").Value) Then .Fields("OTvetstvennyj").Value = rsdb.Fields("OTvetstvennyj").Value
                            If Not IsDBNull(rsdb.Fields("TELEPHONE").Value) Then .Fields("TELEPHONE").Value = rsdb.Fields("TELEPHONE").Value


                            .Fields("NET_NAME").Value = sTEMP0
                            .Fields("PSEVDONIM").Value = sTEMP0
                            .Fields("TIP_COMPA").Value = "ИБП"
                            .Fields("PCL").Value = sSID
                            .Fields("TIPtehn").Value = "IBP"
                            .Fields("INV_NO_PRINTER").Value = rsdb.Fields("INV_NO_IBP").Value

                            .Fields("FILIAL").Value = sBranch
                            .Fields("mesto").Value = sDepartment
                            .Fields("kabn").Value = sOffice

                            .Fields("date").Value = Date.Today
                            .Fields("SFAktNo").Value = 0
                            .Fields("CenaRub").Value = 0
                            .Fields("StoimRub").Value = 0

                            .Fields("Zaiavk").Value = 0

                            .Fields("DataVVoda").Value = rsdb.Fields("DataVVoda").Value
                            .Fields("dataSF").Value = rsdb.Fields("dataSF").Value

                            .Fields("Spisan").Value = 0
                            .Fields("Balans").Value = 0

                            .Update()
                        End With

                        rs.Close()
                        rs = Nothing


                        sSQL = "SELECT * FROM kompy where id=" & sSID

                        rs = New ADODB.Recordset
                        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        With rs
                            .Fields("IBP_NAME").Value = ""
                            .Fields("IBP_SN").Value = ""
                            .Fields("IBP_PROIZV").Value = ""
                            .Fields("INV_NO_IBP").Value = ""
                            .Update()
                        End With

                        rs.Close()
                        rs = Nothing


                        Dim rsBK As ADODB.Recordset
                        rsBK = New ADODB.Recordset
                        rsBK.Open("SELECT id FROM kompy WHERE PRINTER_NAME_1='" & sTEMP0 & "' and MESTO='" & sDepartment & "' and FILIAL='" & sBranch & "'  and kabn='" & sOffice & "' and PCL=" & sSID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        Dim sPRN As String
                        With rsBK

                            sPRN = .Fields("ID").Value

                        End With
                        rsBK.Close()
                        rsBK = Nothing

                        objIniFile.WriteString("general", "DK", sPRN)
                        objIniFile.WriteString("general", "Default", 0)

                    End If
                End If

            End If


            '#################################################
            'Клавиатура+Мышь
            If sTEMP3 = 0 Or sTEMP3 = 4 Then

                sTEMP0 = rsdb.Fields("KEYBOARD_NAME").Value

                If Left(sTEMP0, 2) <> "Мо" And Len(sTEMP0) <> 0 Then

                    If Not RSPRNExt(sTEMP0, sSID) Then

                        sADD = True
                        Call SaveActivityToLogDB("Добавление Клавиатуры в результате разделения " & sTEMP0)
                        Dim rs As ADODB.Recordset
                        Dim sSQL As String


                        If Not RSExists("KEYBOARD", "name", Trim(sTEMP0)) Then
                            AddTwoPar(sTEMP0, rsdb.Fields("KEYBOARD_PROIZV").Value, "SPR_KEYBOARD", frmComputers.cmbOTH)
                        End If


                        sSQL = "SELECT * FROM kompy"

                        rs = New ADODB.Recordset
                        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)


                        With rs

                            .AddNew()


                            .Fields("PRINTER_NAME_1").Value = sTEMP0

                            If Not IsDBNull(rsdb.Fields("KEYBOARD_SN").Value) Then .Fields("PRINTER_SN_1").Value = rsdb.Fields("KEYBOARD_SN").Value
                            If Not IsDBNull(rsdb.Fields("PRINTER_PROIZV_1").Value) Then .Fields("PRINTER_PROIZV_1").Value = rsdb.Fields("KEYBOARD_PROIZV").Value

                            .Fields("port_1").Value = ""

                            If Not IsDBNull(rsdb.Fields("OTvetstvennyj").Value) Then .Fields("OTvetstvennyj").Value = rsdb.Fields("OTvetstvennyj").Value
                            If Not IsDBNull(rsdb.Fields("TELEPHONE").Value) Then .Fields("TELEPHONE").Value = rsdb.Fields("TELEPHONE").Value

                            .Fields("NET_NAME").Value = sTEMP0
                            .Fields("PSEVDONIM").Value = sTEMP0
                            .Fields("TIP_COMPA").Value = "Клавиатура"
                            .Fields("PCL").Value = sSID
                            .Fields("TIPtehn").Value = "KEYB"

                            .Fields("FILIAL").Value = sBranch
                            .Fields("mesto").Value = sDepartment
                            .Fields("kabn").Value = sOffice

                            .Fields("date").Value = Date.Today
                            .Fields("SFAktNo").Value = 0
                            .Fields("CenaRub").Value = 0
                            .Fields("StoimRub").Value = 0

                            .Fields("Zaiavk").Value = 0

                            .Fields("DataVVoda").Value = rsdb.Fields("DataVVoda").Value
                            .Fields("dataSF").Value = rsdb.Fields("dataSF").Value

                            .Fields("Spisan").Value = 0
                            .Fields("Balans").Value = 0

                            .Update()
                        End With

                        rs.Close()
                        rs = Nothing


                        sSQL = "SELECT * FROM kompy where id=" & sSID

                        rs = New ADODB.Recordset
                        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        With rs
                            .Fields("KEYBOARD_NAME").Value = ""
                            .Fields("KEYBOARD_SN").Value = ""
                            .Fields("KEYBOARD_PROIZV").Value = ""
                            .Update()
                        End With

                        rs.Close()
                        rs = Nothing


                        Dim rsBK As ADODB.Recordset
                        rsBK = New ADODB.Recordset
                        rsBK.Open("SELECT id FROM kompy WHERE PRINTER_NAME_1='" & sTEMP0 & "' and MESTO='" & sDepartment & "' and FILIAL='" & sBranch & "'  and kabn='" & sOffice & "' and PCL=" & sSID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        Dim sPRN As String
                        With rsBK

                            sPRN = .Fields("ID").Value

                        End With
                        rsBK.Close()
                        rsBK = Nothing

                        objIniFile.WriteString("general", "DK", sPRN)
                        objIniFile.WriteString("general", "Default", 0)

                    End If
                End If

                '#########################################################
                'Мышь
                sTEMP0 = rsdb.Fields("MOUSE_NAME").Value

                If Left(sTEMP0, 2) <> "Мо" And Len(sTEMP0) <> 0 Then

                    If Not RSPRNExt(sTEMP0, sSID) Then

                        sADD = True
                        Call SaveActivityToLogDB("Добавление Мыши в результате разделения " & sTEMP0)
                        Dim rs As ADODB.Recordset
                        Dim sSQL As String

                        If Not RSExists("MOUSE", "name", Trim(sTEMP0)) Then
                            AddTwoPar(sTEMP0, rsdb.Fields("MOUSE_PROIZV").Value, "SPR_MOUSE", frmComputers.cmbOTH)
                        End If


                        sSQL = "SELECT * FROM kompy"

                        rs = New ADODB.Recordset
                        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)


                        With rs

                            .AddNew()

                            .Fields("PRINTER_NAME_1").Value = sTEMP0
                            .Fields("PRINTER_SN_1").Value = rsdb.Fields("MOUSE_SN").Value
                            .Fields("PRINTER_PROIZV_1").Value = rsdb.Fields("MOUSE_PROIZV").Value
                            .Fields("port_1").Value = ""

                            .Fields("OTvetstvennyj").Value = rsdb.Fields("OTvetstvennyj").Value
                            .Fields("TELEPHONE").Value = rsdb.Fields("TELEPHONE").Value
                            .Fields("NET_NAME").Value = sTEMP0
                            .Fields("PSEVDONIM").Value = sTEMP0
                            .Fields("TIP_COMPA").Value = "Мышь"
                            .Fields("PCL").Value = sSID
                            .Fields("TIPtehn").Value = "MOUSE"

                            .Fields("FILIAL").Value = sBranch
                            .Fields("mesto").Value = sDepartment
                            .Fields("kabn").Value = sOffice

                            .Fields("date").Value = Date.Today
                            .Fields("SFAktNo").Value = 0
                            .Fields("CenaRub").Value = 0
                            .Fields("StoimRub").Value = 0

                            .Fields("Zaiavk").Value = 0

                            .Fields("DataVVoda").Value = rsdb.Fields("DataVVoda").Value
                            .Fields("dataSF").Value = rsdb.Fields("dataSF").Value

                            .Fields("Spisan").Value = 0
                            .Fields("Balans").Value = 0

                            .Update()
                        End With

                        rs.Close()
                        rs = Nothing


                        sSQL = "SELECT * FROM kompy where id=" & sSID

                        rs = New ADODB.Recordset
                        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        With rs
                            .Fields("MOUSE_NAME").Value = ""
                            .Fields("MOUSE_SN").Value = ""
                            .Fields("MOUSE_PROIZV").Value = ""
                            .Update()
                        End With

                        rs.Close()
                        rs = Nothing


                        Dim rsBK As ADODB.Recordset
                        rsBK = New ADODB.Recordset
                        rsBK.Open("SELECT id FROM kompy WHERE PRINTER_NAME_1='" & sTEMP0 & "' and MESTO='" & sDepartment & "' and FILIAL='" & sBranch & "'  and kabn='" & sOffice & "' and PCL=" & sSID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        Dim sPRN As String
                        With rsBK

                            sPRN = .Fields("ID").Value

                        End With
                        rsBK.Close()
                        rsBK = Nothing

                        objIniFile.WriteString("general", "DK", sPRN)
                        objIniFile.WriteString("general", "Default", 0)

                    End If
                End If

            End If

            '#################################################


            '#################################################
            'Сетевой фильтр
            If sTEMP3 = 0 Or sTEMP3 = 5 Then

                sTEMP0 = rsdb.Fields("FILTR_NAME").Value

                If Left(sTEMP0, 2) <> "Мо" And Len(sTEMP0) <> 0 Then

                    If Not RSPRNExt(sTEMP0, sSID) Then

                        sADD = True
                        Call SaveActivityToLogDB("Добавление Сетевого фильтра в результате разделения " & sTEMP0)
                        Dim rs As ADODB.Recordset
                        Dim sSQL As String


                        If Not RSExists("OTHER", "name", Trim("Сетевой фильтр")) Then

                            Dim rsOTH As ADODB.Recordset
                            rsOTH = New ADODB.Recordset
                            rsOTH.Open("Select * from spr_other", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                            With rsOTH
                                .AddNew()
                                .Fields("name").Value = "Сетевой фильтр"
                                .Fields("A").Value = 48
                                .Update()
                            End With
                            rsOTH.Close()
                            rsOTH = Nothing

                        End If

                        If Not RSExists("OTHD", "name", Trim(sTEMP0)) Then
                            AddTreePar(sTEMP0, "Сетевой фильтр", rsdb.Fields("FILTR_PROIZV").Value, "SPR_OTH_DEV", frmComputers.cmbOTH)
                        End If


                        sSQL = "SELECT * FROM kompy"

                        rs = New ADODB.Recordset
                        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)


                        With rs

                            .AddNew()

                            .Fields("PRINTER_NAME_1").Value = sTEMP0
                            .Fields("PRINTER_SN_1").Value = rsdb.Fields("FILTR_SN").Value
                            .Fields("PRINTER_PROIZV_1").Value = rsdb.Fields("FILTR_PROIZV").Value
                            .Fields("port_1").Value = ""

                            .Fields("OTvetstvennyj").Value = rsdb.Fields("OTvetstvennyj").Value
                            .Fields("TELEPHONE").Value = rsdb.Fields("TELEPHONE").Value
                            .Fields("NET_NAME").Value = sTEMP0
                            .Fields("PSEVDONIM").Value = sTEMP0
                            .Fields("TIP_COMPA").Value = "Сетевой фильтр"
                            .Fields("PCL").Value = sSID
                            .Fields("TIPtehn").Value = "OT"

                            .Fields("FILIAL").Value = sBranch
                            .Fields("mesto").Value = sDepartment
                            .Fields("kabn").Value = sOffice

                            .Fields("date").Value = Date.Today
                            .Fields("SFAktNo").Value = 0
                            .Fields("CenaRub").Value = 0
                            .Fields("StoimRub").Value = 0

                            .Fields("Zaiavk").Value = 0

                            .Fields("DataVVoda").Value = rsdb.Fields("DataVVoda").Value
                            .Fields("dataSF").Value = rsdb.Fields("dataSF").Value

                            .Fields("Spisan").Value = 0
                            .Fields("Balans").Value = 0

                            .Update()
                        End With

                        rs.Close()
                        rs = Nothing


                        sSQL = "SELECT * FROM kompy where id=" & sSID

                        rs = New ADODB.Recordset
                        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        With rs
                            .Fields("FILTR_NAME").Value = ""
                            .Fields("FILTR_SN").Value = ""
                            .Fields("FILTR_PROIZV").Value = ""
                            .Update()
                        End With

                        rs.Close()
                        rs = Nothing


                        Dim rsBK As ADODB.Recordset
                        rsBK = New ADODB.Recordset
                        rsBK.Open("SELECT id FROM kompy WHERE PRINTER_NAME_1='" & sTEMP0 & "' and MESTO='" & sDepartment & "' and FILIAL='" & sBranch & "'  and kabn='" & sOffice & "' and PCL=" & sSID, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                        Dim sPRN As String
                        With rsBK

                            sPRN = .Fields("ID").Value

                        End With
                        rsBK.Close()
                        rsBK = Nothing

                        objIniFile.WriteString("general", "DK", sPRN)
                        objIniFile.WriteString("general", "Default", 0)

                    End If
                End If


            End If





        End With

        rsdb.Close()
        rsdb = Nothing



        If MRZD = True Then Exit Sub

        If sADD = True Then
            RefFilTree(frmComputers.lstGroups)

        End If


        Exit Sub

err_:
        'MsgBox(Err.Description)

        If MRZD = True Then Exit Sub

        If sADD = True Then
            RefFilTree(frmComputers.lstGroups)

        End If

    End Sub

    Private Function RSPRNExt(ByVal sPRN As String, ByVal sPCL As String)

        RSPRNExt = False

        Dim rs As ADODB.Recordset
        Dim sSQL As String

        sSQL = "SELECT count(*) as t_n FROM kompy where NET_NAME ='" & sPRN & "' and PCL=" & sPCL

        rs = New ADODB.Recordset
        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)


        With rs
            If .Fields("t_n").Value = 0 Then

            Else

                RSPRNExt = True
                rs.Close()
                rs = Nothing
                Exit Function

            End If
        End With


        RSPRNExt = False

        Exit Function
Error_:
        RSPRNExt = False

    End Function

    Public Sub NotesEditAdd(ByVal btAdd As Button, ByVal lvsNotes As ListView, ByVal NotesMaster As ComboBox, ByVal textNotes As TextBox, ByVal DateNotes As DateTimePicker, ByVal txtSNAME As String, ByVal Branch As ComboBox, ByVal Department As ComboBox, ByVal Office As ComboBox)

        On Error Resume Next

        If Len(textNotes.Text) = 0 Then Exit Sub

        Dim sSQL As String
        Dim langfile As New IniFile(sLANGPATH)


        If btAdd.Text = langfile.GetString("frmComputers", "MSG30", "") Then
            Call SaveActivityToLogDB(langfile.GetString("frmComputers", "MSG45", "") & " " & frmComputers.lstGroups.SelectedNode.Text)
            sSQL = "Select * from Zametki"

        Else
            Call SaveActivityToLogDB(langfile.GetString("frmComputers", "MSG46", "") & " " & frmComputers.lstGroups.SelectedNode.Text)
            sSQL = "Select * from Zametki WHERE id =" & frmComputers.zCOUNT

        End If



        Dim uname As String

        uname = Branch.Text

        If Len(Department.Text) <> 0 Then
            uname = uname & "/" & Department.Text
        End If

        If Len(Office.Text) <> 0 Then
            uname = uname & "/" & Office.Text
        End If

        Dim rs As ADODB.Recordset
        rs = New ADODB.Recordset
        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        With rs

            If btAdd.Text = langfile.GetString("frmComputers", "MSG42", "") Then
                '.Edit
            Else
                .AddNew()
                .Fields("NomerZamKomp").Value = lvsNotes.Items.Count + 1
            End If

            .Fields("Master").Value = NotesMaster.Text
            .Fields("Zametki").Value = textNotes.Text
            .Fields("Date").Value = DateNotes.Value

            .Fields("Id_Comp").Value = frmComputers.sCOUNT

            .Fields("Comp_name").Value = txtSNAME
            .Fields("Mesto_Compa").Value = uname

            .Update()
        End With

        rs.Close()
        rs = Nothing

        btAdd.Text = langfile.GetString("frmComputers", "MSG30", "")
        DateNotes.Value = Date.Today
        NotesMaster.Text = ""
        textNotes.Text = ""

        Call LOAD_NOTES(frmComputers.sCOUNT, lvsNotes)

    End Sub

    Public Sub NET_PORT_ED(Optional ByVal sSID As String = "")


        Dim langfile As New IniFile(sLANGPATH)



        If frmComputers.portEDT = True Then
        Else
            If frmComputers.lvNetPort.Items.Count >= frmComputers.txtNetPort.Text Then
                MsgBox(langfile.GetString("frmComputers", "MSG47", ""), MsgBoxStyle.Information, ProGramName)
                Exit Sub
            End If
        End If

        Dim rs As ADODB.Recordset
        rs = New ADODB.Recordset
        Dim sSQL As String

        If frmComputers.portEDT = True Then
            sSQL = "SELECT * FROM net_port WHERE id=" & sSID
        Else
            sSQL = "SELECT * FROM net_port"
        End If

        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        With rs
            If frmComputers.portEDT = True Then
            Else
                .AddNew()
            End If

            .Fields("id_net").Value = frmComputers.sCOUNT
            .Fields("port").Value = frmComputers.txtNetnumberPort.Text
            .Fields("net_n").Value = frmComputers.txtNetPortMapping.Text
            .Fields("mac").Value = frmComputers.txtNetPortMac.Text
            .Update()

        End With
        rs.Close()
        rs = Nothing
        frmComputers.portEDT = False
        frmComputers.txtNetnumberPort.Text = ""
        frmComputers.txtNetPortMapping.Text = ""
        frmComputers.txtNetPortMac.Text = ""

        LOAD_NET_PORT(frmComputers.sCOUNT)

    End Sub

    Public Sub User_Comp_ADD()
        On Error Resume Next
        Dim langfile As New IniFile(sLANGPATH)



        If frmComputers.sCOUNT = 0 Then Exit Sub
        Dim Us1 As String
        Dim Us2 As String

        If frmComputers.cmdUserAdd.Text = langfile.GetString("frmComputers", "MSG42", "") Then

            Dim USERCOMP As ADODB.Recordset
            USERCOMP = New ADODB.Recordset
            USERCOMP.Open("SELECT * FROM USER_COMP WHERE id=" & frmComputers.uCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

            With USERCOMP
                If Not IsDBNull(.Fields("PASSWORD")) Then Us1 = .Fields("PASSWORD").Value
                If Not IsDBNull(.Fields("EPASS")) Then Us2 = .Fields("EPASS").Value
            End With
            USERCOMP.Close()
            USERCOMP = Nothing


        End If

        Dim sSQL As String
        Dim rs As ADODB.Recordset
        rs = New ADODB.Recordset

        If frmComputers.cmdUserAdd.Text = langfile.GetString("frmComputers", "MSG30", "") Then
            Call SaveActivityToLogDB(langfile.GetString("frmComputers", "MSG48", "") & " " & frmComputers.lstGroups.SelectedNode.Text)
            sSQL = "Select * from USER_COMP"
        Else
            Call SaveActivityToLogDB(langfile.GetString("frmComputers", "MSG49", "") & " " & frmComputers.lstGroups.SelectedNode.Text)
            sSQL = "Select * from USER_COMP WHERE id =" & frmComputers.uCOUNT
        End If

        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        With rs

            If frmComputers.cmdUserAdd.Text = langfile.GetString("frmComputers", "MSG42", "") Then
                '.Edit
            Else
                .AddNew()
            End If

            .Fields("Id_Comp").Value = frmComputers.sCOUNT
            .Fields("USERNAME").Value = frmComputers.txtUserName.Text
            .Fields("EMAIL").Value = frmComputers.txtUserEmail.Text
            .Fields("FIO").Value = frmComputers.txtUserFIO.Text
            .Fields("icq").Value = frmComputers.txtUserIcq.Text

            If frmComputers.cmdUserAdd.Text = langfile.GetString("frmComputers", "MSG42", "") Then


                If Us1 = frmComputers.txtUserPass.Text Or Us1 = Nothing Then

                Else
                    strPassword = frmComputers.txtUserPass.Text
                    EncryptDecrypt(strPassword)
                    .Fields("PASSWORD").Value = Temp$
                End If

                If Us2 = frmComputers.txtUserEmailPwd.Text Or Us2 = Nothing Then

                Else
                    strPassword = frmComputers.txtUserEmailPwd.Text
                    EncryptDecrypt(strPassword)
                    .Fields("EPASS").Value = Temp$
                End If

            Else
                strPassword = Trim(frmComputers.txtUserEmailPwd.Text)
                Call EncryptDecrypt(strPassword)
                frmComputers.txtUserEmailPwd.Text = Temp$

                strPassword = Trim(frmComputers.txtUserPass.Text)
                Call EncryptDecrypt(strPassword)
                frmComputers.txtUserPass.Text = Temp$

                .Fields("PASSWORD").Value = frmComputers.txtUserPass.Text
                .Fields("EPASS").Value = frmComputers.txtUserEmailPwd.Text
            End If

            .Fields("MEMO").Value = frmComputers.txtUMEMO.Text

            If frmComputers.ChkPDC.Checked = True Then
                .Fields("PDC").Value = True
            Else
                .Fields("PDC").Value = False
            End If


            .Update()
        End With

        rs.Close()
        rs = Nothing

        If Not RSExists("USER", "name", Trim(frmComputers.txtUserName.Text)) Then


            rs = New ADODB.Recordset

            rs.Open("Select * from SPR_USER ", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

            With rs
                .AddNew()
                .Fields("name").Value = frmComputers.txtUserName.Text
                .Fields("A").Value = frmComputers.txtUserFIO.Text
                .Update()
            End With
            rs.Close()
            rs = Nothing

            frmComputers.txtUserName.Items.Add(frmComputers.txtUserName.Text)

        End If










        frmComputers.cmdUserAdd.Text = langfile.GetString("frmComputers", "MSG30", "")

        frmComputers.txtUserName.Text = ""
        frmComputers.txtUserPass.Text = ""
        frmComputers.txtUserEmail.Text = ""
        frmComputers.txtUserEmailPwd.Text = ""
        frmComputers.txtUserFIO.Text = ""
        frmComputers.txtUserIcq.Text = ""
        frmComputers.txtUMEMO.Text = ""

        LOAD_USER(frmComputers.sCOUNT)
    End Sub

    Public Sub BR_NOTES_ADD()

        If Len(frmComputers.Notesbrtxt.Text) = 0 Then Exit Sub

        Dim StrS As String

        Dim langfile As New IniFile(sLANGPATH)


        Dim rs As ADODB.Recordset


        Select Case frmComputers.sPREF

            Case "C"

            Case "G"
                StrS = frmComputers.sCOUNT & "F"

            Case "O"
                StrS = frmComputers.sCOUNT & "O_F"

            Case "K"
                StrS = frmComputers.sCOUNT & "K"

        End Select


        If frmComputers.btnBRNotesAdd.Text = langfile.GetString("frmComputers", "MSG42", "") Then

            Dim z As Integer

            For z = 0 To frmComputers.lvNotesBR.SelectedItems.Count - 1
                frmComputers.zCOUNT = (frmComputers.lvNotesBR.SelectedItems(z).Text)
            Next

            rs = New ADODB.Recordset
            rs.Open("SELECT * FROM ZAM_OTD WHERE id =" & frmComputers.zCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        Else
            rs = New ADODB.Recordset
            rs.Open("SELECT * FROM ZAM_OTD", DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        End If

        With rs

            If frmComputers.btnBRNotesAdd.Text = langfile.GetString("frmComputers", "MSG42", "") Then
                '.Edit
            Else
                .AddNew()
                .Fields("ID_ZAM").Value = frmComputers.lvNotesBR.Items.Count + 1
                .Fields("ID_OTD").Value = StrS
            End If

            .Fields("DATE").Value = frmComputers.Notesbrdate.Value
            .Fields("ZAMETKA").Value = frmComputers.Notesbrtxt.Text
            .Fields("Master").Value = frmComputers.cmbBRMaster.Text
            '.Fields("uroven").Value = rem_U.Text
            '.Fields("Vip".Value) = cmbVip.Text
            .Update()
        End With

        rs.Close()
        rs = Nothing

        LOAD_INF_BRANCHE(frmComputers.sCOUNT)
        Notes_Clear(frmComputers.btnBRNotesAdd, frmComputers.Notesbrdate, frmComputers.cmbBRMaster, frmComputers.Notesbrtxt)

    End Sub
End Module
