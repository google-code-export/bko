﻿Public Class frmGarCPL
    Public sTEXT As String

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Me.Close()

    End Sub

    Private Sub frmGarCPL_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Call LANG_frmGAR_CPL()

        Dim objIniFile As New IniFile(sLANGPATH)



        FillComboNET(Me.cmbPostav, "Name", "SPR_Postav", "", False, True)
        FillComboNET(Me.PROizV1, "PROIZV", "SPR_PROIZV", "", False, True)
        FillComboNET(Me.PROizV2, "PROIZV", "SPR_PROIZV", "", False, True)
        FillComboNET(Me.PROizV3, "PROIZV", "SPR_PROIZV", "", False, True)
        FillComboNET(Me.PROizV4, "PROIZV", "SPR_PROIZV", "", False, True)

        gbcpu.Height = 129
        gbPost.Top = 135
        btnAdd.Top = 236
        btnCancel.Top = 236
        Me.Height = 287
        txtMHZ1.Width = 100
        txtSoc1.Visible = True

        cmbCPU2.Enabled = True
        cmbCPU3.Enabled = True
        cmbCPU4.Enabled = True
        txtMHZ2.Enabled = True
        txtMHZ3.Enabled = True
        txtMHZ4.Enabled = True
        PROizV2.Enabled = True
        PROizV3.Enabled = True
        PROizV4.Enabled = True
        txtSoc2.Enabled = True
        txtSoc3.Enabled = True
        txtSoc4.Enabled = True

        cmbCPU2.Visible = True
        txtSoc2.Visible = True
        PROizV2.Visible = True

        txtMHZ2.Left = txtMHZ1.Left
        txtMHZ2.Width = txtMHZ1.Width



        Select Case sTEXT

            Case "CPU"
                gbcpu.Text = objIniFile.GetString("frmGarCPL", "MSG1", "")

                FillComboNET(Me.cmbCPU1, "Name", "SPR_CPU", "", False, True)
                FillComboNET(Me.cmbCPU2, "Name", "SPR_CPU", "", False, True)
                FillComboNET(Me.cmbCPU3, "Name", "SPR_CPU", "", False, True)
                FillComboNET(Me.cmbCPU4, "Name", "SPR_CPU", "", False, True)

            Case "RAM"
                gbcpu.Text = objIniFile.GetString("frmGarCPL", "MSG2", "")
                FillComboNET(Me.cmbCPU1, "Name", "SPR_RAM", "", False, True)
                FillComboNET(Me.cmbCPU2, "Name", "SPR_RAM", "", False, True)
                FillComboNET(Me.cmbCPU3, "Name", "SPR_RAM", "", False, True)
                FillComboNET(Me.cmbCPU4, "Name", "SPR_RAM", "", False, True)

            Case "HDD"
                gbcpu.Text = objIniFile.GetString("frmGarCPL", "MSG3", "")
                FillComboNET(Me.cmbCPU1, "Name", "SPR_HDD", "", False, True)
                FillComboNET(Me.cmbCPU2, "Name", "SPR_HDD", "", False, True)
                FillComboNET(Me.cmbCPU3, "Name", "SPR_HDD", "", False, True)
                FillComboNET(Me.cmbCPU4, "Name", "SPR_HDD", "", False, True)

            Case "SVGA"
                gbcpu.Text = objIniFile.GetString("frmGarCPL", "MSG4", "")
                cmbCPU2.Enabled = False
                cmbCPU3.Enabled = False
                cmbCPU4.Enabled = False

                txtMHZ2.Enabled = False
                txtMHZ3.Enabled = False
                txtMHZ4.Enabled = False

                txtSoc2.Enabled = False
                txtSoc3.Enabled = False
                txtSoc4.Enabled = False

                PROizV2.Enabled = False
                PROizV3.Enabled = False
                PROizV4.Enabled = False

                gbcpu.Height = 73
                gbPost.Top = 81
                btnAdd.Top = 178
                btnCancel.Top = 178
                Me.Height = 231


                FillComboNET(Me.cmbCPU1, "Name", "SPR_SVGA", "", False, True)
                FillComboNET(Me.cmbCPU2, "Name", "SPR_SVGA", "", False, True)

            Case "SOUND"
                gbcpu.Text = objIniFile.GetString("frmGarCPL", "MSG5", "")

                gbcpu.Height = 44
                gbPost.Top = 52
                btnAdd.Top = 149
                btnCancel.Top = 149
                Me.Height = 198

                cmbCPU2.Enabled = False
                cmbCPU3.Enabled = False
                cmbCPU4.Enabled = False
                txtMHZ2.Enabled = False
                txtMHZ3.Enabled = False
                txtMHZ4.Enabled = False
                txtSoc2.Enabled = False
                txtSoc3.Enabled = False
                txtSoc4.Enabled = False
                PROizV2.Enabled = False
                PROizV3.Enabled = False
                PROizV4.Enabled = False

                FillComboNET(Me.cmbCPU1, "Name", "SPR_SOUND", "", False, True)

            Case "MB"
                gbcpu.Text = objIniFile.GetString("frmGarCPL", "MSG6", "")
                txtMHZ1.Width = 205
                txtSoc1.Visible = False
                cmbCPU2.Visible = False
                txtSoc2.Visible = False
                PROizV2.Visible = False
                txtMHZ2.Left = cmbCPU1.Left
                txtMHZ2.Width = 642

                gbcpu.Height = 73
                gbPost.Top = 81
                btnAdd.Top = 178
                btnCancel.Top = 178
                Me.Height = 231

                cmbCPU2.Enabled = False
                cmbCPU3.Enabled = False
                cmbCPU4.Enabled = False

                txtMHZ3.Enabled = False
                txtMHZ4.Enabled = False

                txtSoc2.Enabled = False
                txtSoc3.Enabled = False
                txtSoc4.Enabled = False
                PROizV2.Enabled = False
                PROizV3.Enabled = False
                PROizV4.Enabled = False

                FillComboNET(Me.cmbCPU1, "Name", "SPR_MB", "", False, True)


            Case "OPTICAL"

                gbcpu.Height = 98
                gbPost.Top = 102
                btnAdd.Top = 198
                btnCancel.Top = 198
                Me.Height = 248

                cmbCPU4.Enabled = False
                txtMHZ4.Enabled = False
                txtSoc4.Enabled = False
                PROizV4.Enabled = False

                gbcpu.Text = objIniFile.GetString("frmGarCPL", "MSG7", "")
                FillComboNET(Me.cmbCPU1, "Name", "SPR_OPTICAL", "", False, True)
                FillComboNET(Me.cmbCPU2, "Name", "SPR_OPTICAL", "", False, True)
                FillComboNET(Me.cmbCPU3, "Name", "SPR_OPTICAL", "", False, True)

            Case "NET"
                gbcpu.Text = objIniFile.GetString("frmGarCPL", "MSG8", "")

                cmbCPU3.Enabled = False
                cmbCPU4.Enabled = False

                txtMHZ3.Enabled = False
                txtMHZ4.Enabled = False

                txtSoc3.Enabled = False
                txtSoc4.Enabled = False

                PROizV3.Enabled = False
                PROizV4.Enabled = False

                gbcpu.Height = 73
                gbPost.Top = 81
                btnAdd.Top = 178
                btnCancel.Top = 178
                Me.Height = 231

                FillComboNET(Me.cmbCPU1, "Name", "SPR_NET", "", False, True)
                FillComboNET(Me.cmbCPU2, "Name", "SPR_NET", "", False, True)

            Case "FDD"
                gbcpu.Text = objIniFile.GetString("frmGarCPL", "MSG9", "")

                gbcpu.Height = 44
                gbPost.Top = 52
                btnAdd.Top = 149
                btnCancel.Top = 149
                Me.Height = 198

                cmbCPU2.Enabled = False
                cmbCPU3.Enabled = False
                cmbCPU4.Enabled = False
                txtMHZ2.Enabled = False
                txtMHZ3.Enabled = False
                txtMHZ4.Enabled = False
                txtSoc2.Enabled = False
                txtSoc3.Enabled = False
                txtSoc4.Enabled = False
                PROizV2.Enabled = False
                PROizV3.Enabled = False
                PROizV4.Enabled = False

                FillComboNET(Me.cmbCPU1, "Name", "SPR_FDD", "", False, True)

            Case "MODEM"
                gbcpu.Text = objIniFile.GetString("frmGarCPL", "MSG10", "")

                gbcpu.Height = 44
                gbPost.Top = 52
                btnAdd.Top = 149
                btnCancel.Top = 149
                Me.Height = 198

                cmbCPU2.Enabled = False
                cmbCPU3.Enabled = False
                cmbCPU4.Enabled = False
                txtMHZ2.Enabled = False
                txtMHZ3.Enabled = False
                txtMHZ4.Enabled = False
                txtSoc2.Enabled = False
                txtSoc3.Enabled = False
                txtSoc4.Enabled = False
                PROizV2.Enabled = False
                PROizV3.Enabled = False
                PROizV4.Enabled = False

                FillComboNET(Me.cmbCPU1, "Name", "SPR_MODEM", "", False, True)

            Case "MONITOR"
                gbcpu.Text = objIniFile.GetString("frmGarCPL", "MSG11", "")

                cmbCPU3.Enabled = False
                cmbCPU4.Enabled = False

                txtMHZ3.Enabled = False
                txtMHZ4.Enabled = False

                txtSoc3.Enabled = False
                txtSoc4.Enabled = False

                PROizV3.Enabled = False
                PROizV4.Enabled = False

                gbcpu.Height = 73
                gbPost.Top = 81
                btnAdd.Top = 178
                btnCancel.Top = 178
                Me.Height = 231

                FillComboNET(Me.cmbCPU1, "Name", "SPR_MONITOR", "", False, True)
                FillComboNET(Me.cmbCPU2, "Name", "SPR_MONITOR", "", False, True)


            Case "KEYBOARD"
                gbcpu.Text = objIniFile.GetString("frmGarCPL", "MSG12", "")

                gbcpu.Height = 44
                gbPost.Top = 52
                btnAdd.Top = 149
                btnCancel.Top = 149
                Me.Height = 198

                cmbCPU2.Enabled = False
                cmbCPU3.Enabled = False
                cmbCPU4.Enabled = False
                txtMHZ2.Enabled = False
                txtMHZ3.Enabled = False
                txtMHZ4.Enabled = False
                txtSoc2.Enabled = False
                txtSoc3.Enabled = False
                txtSoc4.Enabled = False
                PROizV2.Enabled = False
                PROizV3.Enabled = False
                PROizV4.Enabled = False

                FillComboNET(Me.cmbCPU1, "Name", "SPR_KEYBOARD", "", False, True)

            Case "MOUSE"
                gbcpu.Text = objIniFile.GetString("frmGarCPL", "MSG13", "")

                gbcpu.Height = 44
                gbPost.Top = 52
                btnAdd.Top = 149
                btnCancel.Top = 149
                Me.Height = 198

                cmbCPU2.Enabled = False
                cmbCPU3.Enabled = False
                cmbCPU4.Enabled = False
                txtMHZ2.Enabled = False
                txtMHZ3.Enabled = False
                txtMHZ4.Enabled = False
                txtSoc2.Enabled = False
                txtSoc3.Enabled = False
                txtSoc4.Enabled = False
                PROizV2.Enabled = False
                PROizV3.Enabled = False
                PROizV4.Enabled = False

                FillComboNET(Me.cmbCPU1, "Name", "SPR_MOUSE", "", False, True)

            Case "FS"
                gbcpu.Text = objIniFile.GetString("frmGarCPL", "MSG14", "")

                gbcpu.Height = 44
                gbPost.Top = 52
                btnAdd.Top = 149
                btnCancel.Top = 149
                Me.Height = 198

                cmbCPU2.Enabled = False
                cmbCPU3.Enabled = False
                cmbCPU4.Enabled = False
                txtMHZ2.Enabled = False
                txtMHZ3.Enabled = False
                txtMHZ4.Enabled = False
                txtSoc2.Enabled = False
                txtSoc3.Enabled = False
                txtSoc4.Enabled = False
                PROizV2.Enabled = False
                PROizV3.Enabled = False
                PROizV4.Enabled = False

                FillComboNET(Me.cmbCPU1, "Name", "SPR_FS", "", False, True)


            Case "USB"
                gbcpu.Text = objIniFile.GetString("frmGarCPL", "MSG15", "")

                gbcpu.Height = 44
                gbPost.Top = 52
                btnAdd.Top = 149
                btnCancel.Top = 149
                Me.Height = 198

                cmbCPU2.Enabled = False
                cmbCPU3.Enabled = False
                cmbCPU4.Enabled = False
                txtMHZ2.Enabled = False
                txtMHZ3.Enabled = False
                txtMHZ4.Enabled = False
                txtSoc2.Enabled = False
                txtSoc3.Enabled = False
                txtSoc4.Enabled = False
                PROizV2.Enabled = False
                PROizV3.Enabled = False
                PROizV4.Enabled = False

                FillComboNET(Me.cmbCPU1, "Name", "SPR_USB", "", False, True)





            Case Else


        End Select



    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        On Error GoTo err_


        Dim sSQL As String


        If Not (RSExists("Postav", "name", Me.cmbPostav.Text)) Then
            AddOnePar(Me.cmbPostav.Text, "NAME", "SPR_Postav", Me.cmbPostav)
        End If


        Select Case sTEXT

            Case "CPU"

                Dim rs As ADODB.Recordset
                rs = New ADODB.Recordset
                rs.Open("SELECT * FROM kompy WHERE id =" & frmComputers.sCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    .Fields("CPU1").Value = Me.cmbCPU1.Text
                    .Fields("CPUmhz1").Value = Me.txtMHZ1.Text
                    .Fields("CPUSocket1").Value = Me.txtSoc1.Text
                    .Fields("CPUProizv1").Value = Me.PROizV1.Text

                    .Fields("CPU2").Value = Me.cmbCPU2.Text
                    .Fields("CPUmhz2").Value = Me.txtMHZ2.Text
                    .Fields("CPUSocket2").Value = Me.txtSoc2.Text
                    .Fields("CPUProizv2").Value = Me.PROizV2.Text

                    .Fields("CPU3").Value = Me.cmbCPU3.Text
                    .Fields("CPUmhz3").Value = Me.txtMHZ3.Text
                    .Fields("CPUSocket3").Value = Me.txtSoc3.Text
                    .Fields("CPUProizv3").Value = Me.PROizV3.Text

                    .Fields("CPU4").Value = Me.cmbCPU4.Text
                    .Fields("CPUmhz4").Value = Me.txtMHZ4.Text
                    .Fields("CPUSocket4").Value = Me.txtSoc4.Text
                    .Fields("CPUProizv4").Value = Me.PROizV4.Text
                    .Update()
                End With
                rs.Close()
                rs = Nothing
                LOADt(frmComputers.sCOUNT)




                If EditGarant = True Then
                    sSQL = "SELECT * FROM garant_comp WHERE Id_Comp =" & frmComputers.sCOUNT
                Else
                    sSQL = "SELECT * FROM garant_comp"
                End If

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    If EditGarant = False Then
                        .AddNew()
                        .Fields("Id_Comp").Value = frmComputers.sCOUNT
                    End If

                    .Fields("CPU_DP").Value = Me.DNG.Value.Day
                    .Fields("CPU_MP").Value = Me.DNG.Value.Month
                    .Fields("CPU_GP").Value = Me.DNG.Value.Year

                    .Fields("CPU_DPo").Value = Me.DOG.Value.Day
                    .Fields("CPU_MPo").Value = Me.DOG.Value.Month
                    .Fields("CPU_GPo").Value = Me.DOG.Value.Year
                    .Fields("CPU_POST").Value = Me.cmbPostav.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing

            Case "RAM"

                Dim rs As ADODB.Recordset
                rs = New ADODB.Recordset
                rs.Open("SELECT * FROM kompy WHERE id =" & frmComputers.sCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                With rs

                    .Fields("RAM_1").Value = Me.cmbCPU1.Text
                    .Fields("RAM_speed_1").Value = Me.txtMHZ1.Text
                    .Fields("RAM_SN_1").Value = Me.txtSoc1.Text
                    .Fields("RAM_PROIZV_1").Value = Me.PROizV1.Text

                    .Fields("RAM_2").Value = Me.cmbCPU2.Text
                    .Fields("RAM_speed_2").Value = Me.txtMHZ2.Text
                    .Fields("RAM_SN_2").Value = Me.txtSoc2.Text
                    .Fields("RAM_PROIZV_2").Value = Me.PROizV2.Text

                    .Fields("RAM_3").Value = Me.cmbCPU4.Text
                    .Fields("RAM_speed_3").Value = Me.txtMHZ3.Text
                    .Fields("RAM_SN_3").Value = Me.txtSoc3.Text
                    .Fields("RAM_PROIZV_3").Value = Me.PROizV3.Text

                    .Fields("RAM_4").Value = Me.cmbCPU4.Text
                    .Fields("RAM_speed_4").Value = Me.txtMHZ4.Text
                    .Fields("RAM_SN_4").Value = Me.txtSoc4.Text
                    .Fields("RAM_PROIZV_4").Value = Me.PROizV4.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing
                LOADt(frmComputers.sCOUNT)

                If EditGarant = True Then
                    sSQL = "SELECT * FROM garant_comp WHERE Id_Comp =" & frmComputers.sCOUNT
                Else
                    sSQL = "SELECT * FROM garant_comp"
                End If

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    If EditGarant = False Then
                        .AddNew()
                        .Fields("Id_Comp").Value = frmComputers.sCOUNT
                    End If

                    .Fields("ram_DP").Value = Me.DNG.Value.Day
                    .Fields("ram_MP").Value = Me.DNG.Value.Month
                    .Fields("ram_GP").Value = Me.DNG.Value.Year

                    .Fields("ram_DPo").Value = Me.DOG.Value.Day
                    .Fields("ram_MPo").Value = Me.DOG.Value.Month
                    .Fields("ram_GPo").Value = Me.DOG.Value.Year
                    .Fields("ram_POST").Value = Me.cmbPostav.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing

            Case "HDD"


                Dim rs As ADODB.Recordset
                rs = New ADODB.Recordset
                rs.Open("SELECT * FROM kompy WHERE id =" & frmComputers.sCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                With rs

                    .Fields("HDD_Name_1").Value = Me.cmbCPU1.Text
                    .Fields("HDD_OB_1").Value = Me.txtMHZ1.Text
                    .Fields("HDD_SN_1").Value = Me.txtSoc1.Text
                    .Fields("HDD_PROIZV_1").Value = Me.PROizV1.Text

                    .Fields("HDD_Name_2").Value = Me.cmbCPU2.Text
                    .Fields("HDD_OB_2").Value = Me.txtMHZ2.Text
                    .Fields("HDD_SN_2").Value = Me.txtSoc2.Text
                    .Fields("HDD_PROIZV_2").Value = Me.PROizV2.Text

                    .Fields("HDD_Name_3").Value = Me.cmbCPU4.Text
                    .Fields("HDD_OB_3").Value = Me.txtMHZ3.Text
                    .Fields("HDD_SN_3").Value = Me.txtSoc3.Text
                    .Fields("HDD_PROIZV_3").Value = Me.PROizV3.Text

                    .Fields("HDD_Name_4").Value = Me.cmbCPU4.Text
                    .Fields("HDD_OB_4").Value = Me.txtMHZ4.Text
                    .Fields("HDD_SN_4").Value = Me.txtSoc4.Text
                    .Fields("HDD_PROIZV_4").Value = Me.PROizV4.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing
                LOADt(frmComputers.sCOUNT)

                If EditGarant = True Then
                    sSQL = "SELECT * FROM garant_comp WHERE Id_Comp =" & frmComputers.sCOUNT
                Else
                    sSQL = "SELECT * FROM garant_comp"
                End If

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    If EditGarant = False Then
                        .AddNew()
                        .Fields("Id_Comp").Value = frmComputers.sCOUNT
                    End If

                    .Fields("HDD_DP").Value = Me.DNG.Value.Day
                    .Fields("HDD_MP").Value = Me.DNG.Value.Month
                    .Fields("HDD_GP").Value = Me.DNG.Value.Year

                    .Fields("HDD_DPo").Value = Me.DOG.Value.Day
                    .Fields("HDD_MPo").Value = Me.DOG.Value.Month
                    .Fields("HDD_GPo").Value = Me.DOG.Value.Year
                    .Fields("HDD_POST").Value = Me.cmbPostav.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing


            Case "SVGA"

                Dim rs As ADODB.Recordset
                rs = New ADODB.Recordset
                rs.Open("SELECT * FROM kompy WHERE id =" & frmComputers.sCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                With rs

                    .Fields("SVGA_NAME").Value = Me.cmbCPU1.Text
                    .Fields("SVGA_OB_RAM").Value = Me.txtMHZ1.Text
                    .Fields("SVGA_SN").Value = Me.txtSoc1.Text
                    .Fields("SVGA_PROIZV").Value = Me.PROizV1.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing
                LOADt(frmComputers.sCOUNT)

                If EditGarant = True Then
                    sSQL = "SELECT * FROM garant_comp WHERE Id_Comp =" & frmComputers.sCOUNT
                Else
                    sSQL = "SELECT * FROM garant_comp"
                End If

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    If EditGarant = False Then
                        .AddNew()
                        .Fields("Id_Comp").Value = frmComputers.sCOUNT
                    End If

                    .Fields("SVGA_DP").Value = Me.DNG.Value.Day
                    .Fields("SVGA_MP").Value = Me.DNG.Value.Month
                    .Fields("SVGA_GP").Value = Me.DNG.Value.Year

                    .Fields("SVGA_DPo").Value = Me.DOG.Value.Day
                    .Fields("SVGA_MPo").Value = Me.DOG.Value.Month
                    .Fields("SVGA_GPo").Value = Me.DOG.Value.Year
                    .Fields("SVGA_POST").Value = Me.cmbPostav.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing

            Case "SOUND"

                Dim rs As ADODB.Recordset
                rs = New ADODB.Recordset
                rs.Open("SELECT * FROM kompy WHERE id =" & frmComputers.sCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                With rs

                    .Fields("SOUND_NAME").Value = Me.cmbCPU1.Text
                    .Fields("SOUND_SN").Value = Me.txtMHZ1.Text
                    .Fields("SOUND_PROIZV").Value = Me.PROizV1.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing
                LOADt(frmComputers.sCOUNT)

                If EditGarant = True Then
                    sSQL = "SELECT * FROM garant_comp WHERE Id_Comp =" & frmComputers.sCOUNT
                Else
                    sSQL = "SELECT * FROM garant_comp"
                End If

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    If EditGarant = False Then
                        .AddNew()
                        .Fields("Id_Comp").Value = frmComputers.sCOUNT
                    End If

                    .Fields("SOUND_DP").Value = Me.DNG.Value.Day
                    .Fields("SOUND_MP").Value = Me.DNG.Value.Month
                    .Fields("SOUND_GP").Value = Me.DNG.Value.Year

                    .Fields("SOUND_DPo").Value = Me.DOG.Value.Day
                    .Fields("SOUND_MPo").Value = Me.DOG.Value.Month
                    .Fields("SOUND_GPo").Value = Me.DOG.Value.Year
                    .Fields("SOUND_POST").Value = Me.cmbPostav.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing

            Case "MB"

                Dim rs As ADODB.Recordset
                rs = New ADODB.Recordset
                rs.Open("SELECT * FROM kompy WHERE id =" & frmComputers.sCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                With rs

                    .Fields("Mb").Value = Me.cmbCPU1.Text
                    .Fields("Mb_Chip").Value = Me.txtMHZ1.Text
                    .Fields("Mb_Proizvod").Value = Me.PROizV1.Text
                    .Fields("Mb_Id").Value = Me.txtMHZ2.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing
                LOADt(frmComputers.sCOUNT)

                If EditGarant = True Then
                    sSQL = "SELECT * FROM garant_comp WHERE Id_Comp =" & frmComputers.sCOUNT
                Else
                    sSQL = "SELECT * FROM garant_comp"
                End If

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    If EditGarant = False Then
                        .AddNew()
                        .Fields("Id_Comp").Value = frmComputers.sCOUNT
                    End If

                    .Fields("mb_DP").Value = Me.DNG.Value.Day
                    .Fields("mb_MP").Value = Me.DNG.Value.Month
                    .Fields("mb_GP").Value = Me.DNG.Value.Year

                    .Fields("mb_DPo").Value = Me.DOG.Value.Day
                    .Fields("mb_MPo").Value = Me.DOG.Value.Month
                    .Fields("mb_GPo").Value = Me.DOG.Value.Year
                    .Fields("mb_POST").Value = Me.cmbPostav.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing


            Case "OPTICAL"

                Dim rs As ADODB.Recordset
                rs = New ADODB.Recordset
                rs.Open("SELECT * FROM kompy WHERE id =" & frmComputers.sCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs

                    .Fields("CD_NAME").Value = Me.cmbCPU1.Text
                    .Fields("CD_SPEED").Value = Me.txtMHZ1.Text
                    .Fields("CD_SN").Value = Me.txtSoc1.Text
                    .Fields("CD_PROIZV").Value = Me.PROizV1.Text

                    .Fields("CDRW_NAME").Value = Me.cmbCPU2.Text
                    .Fields("CDRW_SPEED").Value = Me.txtMHZ2.Text
                    .Fields("CDRW_SN").Value = Me.txtSoc2.Text
                    .Fields("CDRW_PROIZV").Value = Me.PROizV2.Text

                    .Fields("DVD_NAME").Value = Me.cmbCPU3.Text
                    .Fields("DVD_SPEED").Value = Me.txtMHZ3.Text
                    .Fields("DVD_SN").Value = Me.txtSoc3.Text
                    .Fields("DVD_PROIZV").Value = Me.PROizV3.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing
                LOADt(frmComputers.sCOUNT)

                If EditGarant = True Then
                    sSQL = "SELECT * FROM garant_comp WHERE Id_Comp =" & frmComputers.sCOUNT
                Else
                    sSQL = "SELECT * FROM garant_comp"
                End If

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    If EditGarant = False Then
                        .AddNew()
                        .Fields("Id_Comp").Value = frmComputers.sCOUNT
                    End If

                    .Fields("CDROM_DP").Value = Me.DNG.Value.Day
                    .Fields("CDROM_MP").Value = Me.DNG.Value.Month
                    .Fields("CDROM_GP").Value = Me.DNG.Value.Year

                    .Fields("CDROM_DPo").Value = Me.DOG.Value.Day
                    .Fields("CDROM_MPo").Value = Me.DOG.Value.Month
                    .Fields("CDROM_GPo").Value = Me.DOG.Value.Year
                    .Fields("CDROM_POST").Value = Me.cmbPostav.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing


            Case "NET"

                Dim rs As ADODB.Recordset
                rs = New ADODB.Recordset
                rs.Open("SELECT * FROM kompy WHERE id =" & frmComputers.sCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                With rs

                    .Fields("NET_NAME_1").Value = Me.cmbCPU1.Text
                    .Fields("NET_IP_1").Value = Me.txtMHZ1.Text
                    .Fields("NET_MAC_1").Value = Me.txtSoc1.Text
                    .Fields("NET_PROIZV_1").Value = Me.PROizV1.Text

                    .Fields("NET_NAME_2").Value = Me.cmbCPU2.Text
                    .Fields("NET_IP_2").Value = Me.txtMHZ2.Text
                    .Fields("NET_MAC_2").Value = Me.txtSoc2.Text
                    .Fields("NET_PROIZV_2").Value = Me.PROizV2.Text
                    .Update()

                End With
                rs.Close()
                rs = Nothing

                LOADt(frmComputers.sCOUNT)

                If EditGarant = True Then
                    sSQL = "SELECT * FROM garant_comp WHERE Id_Comp =" & frmComputers.sCOUNT
                Else
                    sSQL = "SELECT * FROM garant_comp"
                End If

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    If EditGarant = False Then
                        .AddNew()
                        .Fields("Id_Comp").Value = frmComputers.sCOUNT
                    End If

                    .Fields("NET_DP").Value = Me.DNG.Value.Day
                    .Fields("NET_MP").Value = Me.DNG.Value.Month
                    .Fields("NET_GP").Value = Me.DNG.Value.Year

                    .Fields("NET_DPo").Value = Me.DOG.Value.Day
                    .Fields("NET_MPo").Value = Me.DOG.Value.Month
                    .Fields("NET_GPo").Value = Me.DOG.Value.Year
                    .Fields("NET_POST").Value = Me.cmbPostav.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing


            Case "FDD"

                Dim rs As ADODB.Recordset
                rs = New ADODB.Recordset
                rs.Open("SELECT * FROM kompy WHERE id =" & frmComputers.sCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                With rs

                    .Fields("FDD_NAME").Value = Me.cmbCPU1.Text
                    .Fields("FDD_SN").Value = Me.txtMHZ1.Text
                    .Fields("FDD_PROIZV").Value = Me.PROizV1.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing
                LOADt(frmComputers.sCOUNT)

                If EditGarant = True Then
                    sSQL = "SELECT * FROM garant_comp WHERE Id_Comp =" & frmComputers.sCOUNT
                Else
                    sSQL = "SELECT * FROM garant_comp"
                End If

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    If EditGarant = False Then
                        .AddNew()
                        .Fields("Id_Comp").Value = frmComputers.sCOUNT
                    End If

                    .Fields("FDD_DP").Value = Me.DNG.Value.Day
                    .Fields("FDD_MP").Value = Me.DNG.Value.Month
                    .Fields("FDD_GP").Value = Me.DNG.Value.Year

                    .Fields("FDD_DPo").Value = Me.DOG.Value.Day
                    .Fields("FDD_MPo").Value = Me.DOG.Value.Month
                    .Fields("FDD_GPo").Value = Me.DOG.Value.Year
                    .Fields("FDD_POST").Value = Me.cmbPostav.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing

            Case "MODEM"

                Dim rs As ADODB.Recordset
                rs = New ADODB.Recordset
                rs.Open("SELECT * FROM kompy WHERE id =" & frmComputers.sCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                With rs

                    .Fields("MODEM_NAME").Value = Me.cmbCPU1.Text
                    .Fields("MODEM_SN").Value = Me.txtMHZ1.Text
                    .Fields("MODEM_PROIZV").Value = Me.PROizV1.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing
                LOADt(frmComputers.sCOUNT)

                If EditGarant = True Then
                    sSQL = "SELECT * FROM garant_comp WHERE Id_Comp =" & frmComputers.sCOUNT
                Else
                    sSQL = "SELECT * FROM garant_comp"
                End If

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    If EditGarant = False Then
                        .AddNew()
                        .Fields("Id_Comp").Value = frmComputers.sCOUNT
                    End If

                    .Fields("MODEM_DP").Value = Me.DNG.Value.Day
                    .Fields("MODEM_MP").Value = Me.DNG.Value.Month
                    .Fields("MODEM_GP").Value = Me.DNG.Value.Year

                    .Fields("MODEM_DPo").Value = Me.DOG.Value.Day
                    .Fields("MODEM_MPo").Value = Me.DOG.Value.Month
                    .Fields("MODEM_GPo").Value = Me.DOG.Value.Year
                    .Fields("MODEM_POST").Value = Me.cmbPostav.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing


            Case "MONITOR"

                Dim rs As ADODB.Recordset
                rs = New ADODB.Recordset
                rs.Open("SELECT * FROM kompy WHERE id =" & frmComputers.sCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                With rs

                    .Fields("MONITOR_NAME").Value = Me.cmbCPU1.Text
                    .Fields("MONITOR_DUM").Value = Me.txtMHZ1.Text
                    .Fields("MONITOR_SN").Value = Me.txtSoc1.Text
                    .Fields("MONITOR_PROIZV").Value = Me.PROizV1.Text

                    .Fields("MONITOR_NAME2").Value = Me.cmbCPU2.Text
                    .Fields("MONITOR_DUM2").Value = Me.txtMHZ2.Text
                    .Fields("MONITOR_SN2").Value = Me.txtSoc2.Text
                    .Fields("MONITOR_PROIZV2").Value = Me.PROizV2.Text
                    .Update()

                End With
                rs.Close()
                rs = Nothing

                LOADt(frmComputers.sCOUNT)

                If EditGarant = True Then
                    sSQL = "SELECT * FROM garant_comp WHERE Id_Comp =" & frmComputers.sCOUNT
                Else
                    sSQL = "SELECT * FROM garant_comp"
                End If

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    If EditGarant = False Then
                        .AddNew()
                        .Fields("Id_Comp").Value = frmComputers.sCOUNT
                    End If

                    .Fields("MONITOR_DP").Value = Me.DNG.Value.Day
                    .Fields("MONITOR_MP").Value = Me.DNG.Value.Month
                    .Fields("MONITOR_GP").Value = Me.DNG.Value.Year

                    .Fields("MONITOR_DPo").Value = Me.DOG.Value.Day
                    .Fields("MONITOR_MPo").Value = Me.DOG.Value.Month
                    .Fields("MONITOR_GPo").Value = Me.DOG.Value.Year
                    .Fields("MONITOR_POST").Value = Me.cmbPostav.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing


            Case "KEYBOARD"

                Dim rs As ADODB.Recordset
                rs = New ADODB.Recordset
                rs.Open("SELECT * FROM kompy WHERE id =" & frmComputers.sCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                With rs

                    .Fields("KEYBOARD_NAME").Value = Me.cmbCPU1.Text
                    .Fields("KEYBOARD_SN").Value = Me.txtMHZ1.Text
                    .Fields("KEYBOARD_PROIZV").Value = Me.PROizV1.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing
                LOADt(frmComputers.sCOUNT)

                If EditGarant = True Then
                    sSQL = "SELECT * FROM garant_comp WHERE Id_Comp =" & frmComputers.sCOUNT
                Else
                    sSQL = "SELECT * FROM garant_comp"
                End If

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    If EditGarant = False Then
                        .AddNew()
                        .Fields("Id_Comp").Value = frmComputers.sCOUNT
                    End If

                    .Fields("KEYBOARD_DP").Value = Me.DNG.Value.Day
                    .Fields("KEYBOARD_MP").Value = Me.DNG.Value.Month
                    .Fields("KEYBOARD_GP").Value = Me.DNG.Value.Year

                    .Fields("KEYBOARD_DPo").Value = Me.DOG.Value.Day
                    .Fields("KEYBOARD_MPo").Value = Me.DOG.Value.Month
                    .Fields("KEYBOARD_GPo").Value = Me.DOG.Value.Year
                    .Fields("KEYBOARD_POST").Value = Me.cmbPostav.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing

            Case "MOUSE"

                Dim rs As ADODB.Recordset
                rs = New ADODB.Recordset
                rs.Open("SELECT * FROM kompy WHERE id =" & frmComputers.sCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                With rs

                    .Fields("MOUSE_NAME").Value = Me.cmbCPU1.Text
                    .Fields("MOUSE_SN").Value = Me.txtMHZ1.Text
                    .Fields("MOUSE_PROIZV").Value = Me.PROizV1.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing
                LOADt(frmComputers.sCOUNT)

                If EditGarant = True Then
                    sSQL = "SELECT * FROM garant_comp WHERE Id_Comp =" & frmComputers.sCOUNT
                Else
                    sSQL = "SELECT * FROM garant_comp"
                End If

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    If EditGarant = False Then
                        .AddNew()
                        .Fields("Id_Comp").Value = frmComputers.sCOUNT
                    End If

                    .Fields("MOUSE_DP").Value = Me.DNG.Value.Day
                    .Fields("MOUSE_MP").Value = Me.DNG.Value.Month
                    .Fields("MOUSE_GP").Value = Me.DNG.Value.Year

                    .Fields("MOUSE_DPo").Value = Me.DOG.Value.Day
                    .Fields("MOUSE_MPo").Value = Me.DOG.Value.Month
                    .Fields("MOUSE_GPo").Value = Me.DOG.Value.Year
                    .Fields("MOUSE_POST").Value = Me.cmbPostav.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing


            Case "FS"

                Dim rs As ADODB.Recordset
                rs = New ADODB.Recordset
                rs.Open("SELECT * FROM kompy WHERE id =" & frmComputers.sCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                With rs

                    .Fields("FILTR_NAME").Value = Me.cmbCPU1.Text
                    .Fields("FILTR_SN").Value = Me.txtMHZ1.Text
                    .Fields("FILTR_PROIZV").Value = Me.PROizV1.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing
                LOADt(frmComputers.sCOUNT)

                If EditGarant = True Then
                    sSQL = "SELECT * FROM garant_comp WHERE Id_Comp =" & frmComputers.sCOUNT
                Else
                    sSQL = "SELECT * FROM garant_comp"
                End If

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    If EditGarant = False Then
                        .AddNew()
                        .Fields("Id_Comp").Value = frmComputers.sCOUNT
                    End If

                    .Fields("FS_DP").Value = Me.DNG.Value.Day
                    .Fields("FS_MP").Value = Me.DNG.Value.Month
                    .Fields("FS_GP").Value = Me.DNG.Value.Year

                    .Fields("FS_DPo").Value = Me.DOG.Value.Day
                    .Fields("FS_MPo").Value = Me.DOG.Value.Month
                    .Fields("FS_GPo").Value = Me.DOG.Value.Year
                    .Fields("FS_POST").Value = Me.cmbPostav.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing


            Case "USB"

                Dim rs As ADODB.Recordset
                rs = New ADODB.Recordset
                rs.Open("SELECT * FROM kompy WHERE id =" & frmComputers.sCOUNT, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                With rs

                    .Fields("USB_NAME").Value = Me.cmbCPU1.Text
                    .Fields("USB_SN").Value = Me.txtMHZ1.Text
                    .Fields("USB_PROIZV").Value = Me.PROizV1.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing
                LOADt(frmComputers.sCOUNT)

                If EditGarant = True Then
                    sSQL = "SELECT * FROM garant_comp WHERE Id_Comp =" & frmComputers.sCOUNT
                Else
                    sSQL = "SELECT * FROM garant_comp"
                End If

                rs = New ADODB.Recordset
                rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs
                    If EditGarant = False Then
                        .AddNew()
                        .Fields("Id_Comp").Value = frmComputers.sCOUNT
                    End If

                    .Fields("USB_DP").Value = Me.DNG.Value.Day
                    .Fields("USB_MP").Value = Me.DNG.Value.Month
                    .Fields("USB_GP").Value = Me.DNG.Value.Year

                    .Fields("USB_DPo").Value = Me.DOG.Value.Day
                    .Fields("USB_MPo").Value = Me.DOG.Value.Month
                    .Fields("USB_GPo").Value = Me.DOG.Value.Year
                    .Fields("USB_POST").Value = Me.cmbPostav.Text

                    .Update()
                End With
                rs.Close()
                rs = Nothing
        End Select



        Me.Close()

        Exit Sub
err_:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmbCPU1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCPU1.SelectedIndexChanged

         Select sTEXT

            Case "CPU"
                ExLoadParFow(cmbCPU1.Text, txtMHZ1, txtSoc1, PROizV1, "SPR_CPU")

            Case "RAM"
                ExLoadParTree(cmbCPU1.Text, txtMHZ1, PROizV1, "SPR_RAM")

            Case "HDD"

                ExLoadParTree(cmbCPU1.Text, txtMHZ1, PROizV1, "SPR_HDD")

            Case "SVGA"

                ExLoadParTree(cmbCPU1.Text, txtMHZ1, PROizV1, "SPR_SVGA")

            Case "SOUND"
                ExLoadParTwo(cmbCPU1.Text, PROizV1, "SPR_SOUND")

            Case "MB"
                ExLoadParTree(cmbCPU1.Text, txtMHZ1, PROizV1, "SPR_MB")

            Case "OPTICAL"

                ExLoadParTree(cmbCPU1.Text, txtMHZ1, PROizV1, "SPR_OPTICAL")

            Case "NET"

                ExLoadParTwo(cmbCPU1.Text, PROizV1, "SPR_NET")

            Case "FDD"

                ExLoadParTwo(cmbCPU1.Text, PROizV1, "SPR_FDD")

            Case "MODEM"

                ExLoadParTwo(cmbCPU1.Text, PROizV1, "SPR_MODEM")

            Case "MONITOR"
                ExLoadParTree(cmbCPU1.Text, txtMHZ1, PROizV1, "SPR_MONITOR")


            Case "KEYBOARD"

                ExLoadParTwo(cmbCPU1.Text, PROizV1, "SPR_KEYBOARD")

            Case "MOUSE"

                ExLoadParTwo(cmbCPU1.Text, PROizV1, "SPR_MOUSE")

            Case "FS"

                ExLoadParTwo(cmbCPU1.Text, PROizV1, "SPR_FS")

            Case "USB"

                ExLoadParTwo(cmbCPU1.Text, PROizV1, "SPR_USB")

        End Select


    End Sub



    Private Sub ExLoadParFow(ByVal sCombo1 As String, ByVal sText1 As TextBox, ByVal sText2 As TextBox, ByVal sCombo2 As ComboBox, ByVal sTABLE As String)

        On Error Resume Next
        Dim rs As ADODB.Recordset
        Dim sSQL As String
        Dim uNI As String
        rs = New ADODB.Recordset



        sSQL = "SELECT * FROM " & sTABLE & " WHERE Name = '" & sCombo1 & "'"

        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        With rs

            If Not IsDBNull(.Fields("proizv").Value) Then uNI = .Fields("proizv").Value

            Dim PROYZV As ADODB.Recordset
            PROYZV = New ADODB.Recordset
            PROYZV.Open("SELECT * FROM SPR_PROIZV WHERE iD=" & uNI, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

            With PROYZV
                sCombo2.Text = .Fields("proizv").Value
            End With
            PROYZV.Close()
            PROYZV = Nothing

            If Not IsDBNull(.Fields("A").Value) Then sText1.Text = .Fields("A").Value
            If Not IsDBNull(.Fields("B").Value) Then sText2.Text = .Fields("B").Value
        End With

        rs.Close()
        rs = Nothing


    End Sub

    Private Sub ExLoadParTree(ByVal sCombo1 As String, ByVal sText1 As TextBox, ByVal sCombo2 As ComboBox, ByVal sTABLE As String)

        On Error Resume Next
        Dim rs As ADODB.Recordset
        Dim sSQL As String
        Dim uNI As String
        rs = New ADODB.Recordset



        sSQL = "SELECT * FROM " & sTABLE & " WHERE Name = '" & sCombo1 & "'"

        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        With rs

            If Not IsDBNull(.Fields("proizv").Value) Then uNI = .Fields("proizv").Value

            Dim PROYZV As ADODB.Recordset
            PROYZV = New ADODB.Recordset
            PROYZV.Open("SELECT * FROM SPR_PROIZV WHERE iD=" & uNI, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

            With PROYZV
                sCombo2.Text = .Fields("proizv").Value
            End With
            PROYZV.Close()
            PROYZV = Nothing

            If Not IsDBNull(.Fields("A").Value) Then sText1.Text = .Fields("A").Value

        End With

        rs.Close()
        rs = Nothing


    End Sub

    Private Sub ExLoadParTwo(ByVal sCombo1 As String, ByVal sCombo2 As ComboBox, ByVal sTABLE As String)

        On Error Resume Next
        Dim rs As ADODB.Recordset
        Dim sSQL As String
        Dim uNI As String
        rs = New ADODB.Recordset



        sSQL = "SELECT * FROM " & sTABLE & " WHERE Name = '" & sCombo1 & "'"

        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        With rs

            If Not IsDBNull(.Fields("proizv").Value) Then uNI = .Fields("proizv").Value

            Dim PROYZV As ADODB.Recordset
            PROYZV = New ADODB.Recordset
            PROYZV.Open("SELECT * FROM SPR_PROIZV WHERE iD=" & uNI, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

            With PROYZV
                sCombo2.Text = .Fields("proizv").Value
            End With
            PROYZV.Close()
            PROYZV = Nothing

        End With

        rs.Close()
        rs = Nothing


    End Sub

    Private Sub cmbCPU2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCPU2.SelectedIndexChanged
        Select Case sTEXT

            Case "CPU"
                ExLoadParFow(cmbCPU2.Text, txtMHZ2, txtSoc2, PROizV2, "SPR_CPU")

            Case "RAM"
                ExLoadParTree(cmbCPU2.Text, txtMHZ2, PROizV2, "SPR_RAM")

            Case "HDD"

                ExLoadParTree(cmbCPU2.Text, txtMHZ2, PROizV2, "SPR_HDD")

            Case "SVGA"

                ExLoadParTree(cmbCPU2.Text, txtMHZ2, PROizV2, "SPR_SVGA")

            Case "OPTICAL"
                ExLoadParTree(cmbCPU2.Text, txtMHZ2, PROizV2, "SPR_OPTICAL")

            Case "NET"

                ExLoadParTwo(cmbCPU2.Text, PROizV2, "SPR_NET")

            Case "MONITOR"
                ExLoadParTree(cmbCPU2.Text, txtMHZ2, PROizV2, "SPR_MONITOR")

        End Select
    End Sub

    Private Sub cmbCPU3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCPU3.SelectedIndexChanged
        Select Case sTEXT

            Case "CPU"
                ExLoadParFow(cmbCPU3.Text, txtMHZ3, txtSoc3, PROizV3, "SPR_CPU")
            Case "RAM"
                ExLoadParTree(cmbCPU3.Text, txtMHZ3, PROizV3, "SPR_RAM")

            Case "HDD"

                ExLoadParTree(cmbCPU3.Text, txtMHZ3, PROizV3, "SPR_HDD")

            Case "OPTICAL"

                ExLoadParTree(cmbCPU3.Text, txtMHZ3, PROizV3, "SPR_OPTICAL")


        End Select
    End Sub

    Private Sub cmbCPU4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCPU4.SelectedIndexChanged
        Select Case sTEXT

            Case "CPU"
                ExLoadParFow(cmbCPU4.Text, txtMHZ4, txtSoc4, PROizV4, "SPR_CPU")

            Case "RAM"
                ExLoadParTree(cmbCPU4.Text, txtMHZ4, PROizV4, "SPR_RAM")

            Case "HDD"

                ExLoadParTree(cmbCPU4.Text, txtMHZ4, PROizV4, "SPR_HDD")



        End Select
    End Sub
End Class