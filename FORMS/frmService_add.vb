﻿Public Class frmService_add
    Private unamZ As String
    Public REMFU As Boolean
    Public REMED As Boolean

    Private Sub cmbAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAdd.Click
        Dim sSQL As String
        Dim unamZ As String
        Dim objIniFile As New IniFile(sLANGPATH)

        If Not (RSExists("otv", "name", Trim(cmbIst.Text))) Then
            AddOnePar(cmbIst.Text, "NAME", "SPR_OTV", cmbIst)
        End If

        Select Case frmComputers.sPREF

            Case "C"

                Dim rs1 As ADODB.Recordset
                rs1 = New ADODB.Recordset
                sSQL = "SELECT filial, mesto, net_name FROM kompy WHERE id=" & frmComputers.sCOUNT
                rs1.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

                With rs1
                    unamZ = .Fields("filial").Value & "/" & .Fields("mesto").Value
                    frmserviceDesc.rtxtC = .Fields("net_name").Value
                End With

                rs1.Close()
                rs1 = Nothing

        End Select

        Dim rs As ADODB.Recordset


        If cmbAdd.Text = objIniFile.GetString("frmService_add", "MSG1", "") Then
            sSQL = "SELECT * FROM Remont WHERE id=" & frmserviceDesc.rCOUNT

        Else
            sSQL = "SELECT * FROM Remont"
        End If


        rs = New ADODB.Recordset
        rs.Open(sSQL, DB7, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        Dim strTime As String
        strTime = TimeString

        With rs
            If cmbAdd.Text = objIniFile.GetString("frmService_add", "MSG1", "") Then

            Else
                .AddNew()

                .Fields("PREF").Value = frmComputers.sPREF
                .Fields("Id_Comp").Value = frmComputers.sCOUNT
                .Fields("NomerRemKomp").Value = frmserviceDesc.lvRem.Items.Count + 1
                .Fields("starttime").Value = strTime 'Физическое нажатие начала ремонта
                .Fields("startdate").Value = Date.Today 'Физическое нажатие начала ремонта
                .Fields("Id_Comp").Value = frmComputers.sCOUNT
                .Fields("Comp_name").Value = frmserviceDesc.rtxtC 'Имя устройства
                .Fields("Mesto_Compa").Value = unamZ 'Место установки устройства

            End If

            .Fields("Master").Value = cmbMast.Text 'Мастер
            .Fields("Date").Value = dtReg.Value 'Дата регистрации
            .Fields("Remont").Value = txtRem.Text 'Сообщение
            .Fields("UserName").Value = "" 'Текущий пользователь компа
            .Fields("vip").Value = cmbStatus.Text 'Статус
            .Fields("istochnik").Value = cmbIst.Text 'Источник
            .Fields("phone").Value = txtPhone.Text 'Телефон
            .Fields("srok").Value = dtIsp.Value 'Срок исполнения
            .Fields("name_of_remont").Value = txtHead.Text 'Название
            .Fields("otvetstv").Value = cmbOtv.Text 'Ответственный
            .Fields("krit_rem").Value = cmbTip.Text 'Критичность
            .Fields("Uroven").Value = cmbKrit.Text 'Тип
            .Fields("MeMo").Value = txtComent.Text 'Комментарий
            .Fields("Summ").Value = RemCashe.Text 'Сумма

            Select Case chkClose.Checked

                Case True

                    If unamDB = "MS access" Then
                        .Fields("zakryt").Value = True
                    Else
                        .Fields("zakryt").Value = "1"
                    End If

                    .Fields("stopdate").Value = Date.Today 'Физическое нажатие закрытие ремонта
                    .Fields("stoptime").Value = strTime 'Физическое нажатие начала ремонта

                Case False

                    If unamDB = "MS access" Then
                        .Fields("zakryt").Value = False
                    Else
                        .Fields("zakryt").Value = "0"
                    End If

            End Select

            .Fields("UserName").Value = uUSERNAME

            .Update()
        End With
        rs.Close()
        rs = Nothing

        Call REM_CHECK()


        If REMFU = True Then
            Call LOAD_REPAIR(frmComputers.sCOUNT, frmComputers.lvRepair)

            Select Case TipTehn

                Case "PC"
                    frmComputers.sSTAB1.SelectedTab = frmComputers.sSTAB1.TabPages("TabPage7")

                Case Else


            End Select


        Else
            Call frmserviceDesc.LOAD_REPAIR(frmComputers.sCOUNT, frmserviceDesc.lvRem)
        End If


        Me.Close()

    End Sub

    Private Sub cmbCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCancel.Click

        Me.Close()

    End Sub

    Private Sub frmService_add_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objIniFile As New IniFile(sLANGPATH)

        Call frmService_add_Lang()

        If REMED = True Then
            cmbAdd.Text = objIniFile.GetString("frmService_add", "MSG1", "")
            'cmbAdd 
        Else

        End If


        FillComboNET(Me.cmbIst, "Name", "SPR_OTV", "", False, True)
        FillComboNET(Me.cmbMast, "Name", "SPR_Master", "", False, True)
        FillComboNET(Me.cmbOtv, "Name", "SPR_Master", "", False, True)
        FillComboNET(Me.cmbStatus, "name", "spr_vip", "", False, True)
        FillComboNET(Me.cmbKrit, "Uroven", "SPR_Uroven", "", False, True)
        FillComboNET(Me.cmbTip, "name", "spr_tip_z", "", False, True)



        If cmbAdd.Text = objIniFile.GetString("frmService_add", "MSG1", "") Then

            chkClose.Enabled = True

        Else
            chkClose.Enabled = False
        End If

    End Sub
End Class