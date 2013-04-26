''' <summary>
'''sharing_type 0 외근관리  - drpGubun 1  외근관리  
'''             P 일정관리(비공유) - drpGubun 0 일정관리
'''             S 일정관리(공유  ) - drpGubun 0 일정관리
'''Sharing 1 sharing_type S
'''Sharing 0 sharing_type 0, P
''' 
''' 외근관리-> 고객약속
''' 
''' 1. 고객약속
'''  약속일시
'''  수행자
'''  등록자
'''  약속제목 - "고객약속"
'''  약속사유(007-외근사유)
'''  약속장소
'''  세부내용
''' 2. 일정관리
'''  시작시간 - 종료시간
'''  공유/비공유
'''  내용
'''  팀명 and 참석자
'''  
''' 
''' </summary>
''' <remarks></remarks>
Public Class FRM_SCHEDULE

    Private temp As String
    Private temp2 As String = ""
    Private temp_Date As String = ""
    Private sel_day As Label = New Label

    Private Sub FRM_SCHEDULE_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            Call SettoolBar(False, False, True, True, False, True, True)
        Catch ex As Exception
            Call WriteLog(Me.Name & " : " & ex.ToString)
        End Try
    End Sub

    Private Sub FRM_SCHEDULE_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Call Controls_Setting()
            Call Calendar_Setting()
            DPYear.Value = Today
            DPMonth.Value = Today
        Catch ex As Exception
            Call WriteLog(Me.Name & " : " & ex.ToString)
        End Try
    End Sub

    Private Sub Controls_Setting()
        Dim dt As DataTable
        Try
            Me.WindowState = FormWindowState.Maximized
            DPYear.CustomFormat = "yyyy"
            DPMonth.CustomFormat = "MM"
            DPYear.Value = New Date(DPYear.Value.Year, DPMonth.Value.Month, 1)
            DPMonth.Value = New Date(DPYear.Value.Year, DPMonth.Value.Month, 1)


            '고객응대


            '내부일정


            txtTitleInternal.MaxLength = 50
            txtDescInternal.MaxLength = 200
            txtWLocCustomer.MaxLength = 30
            txtTitleInternal.ImeMode = Windows.Forms.ImeMode.Hangul
            txtDescInternal.ImeMode = Windows.Forms.ImeMode.Hangul
            txtWLocCustomer.ImeMode = Windows.Forms.ImeMode.Hangul
            cboUserCustomer.Enabled = False
            cboWReasonCustomer.Enabled = False
            txtWLocCustomer.Enabled = False

            dtpStartInternal.Value = Now
            dtpEndInternal.Value = Now
            CB_Set2(cboHourStartInternal, "datetime", 0, 23, 1, "")
            CB_Set2(cboHourEndInternal, "datetime", 0, 23, 1, "")
            CB_Set2(cboMinStartInternal, "datetime", 0, 50, 10, "")
            CB_Set2(cboMinEndInternal, "datetime", 0, 50, 10, "")
            cboHourStartInternal.Text = "00"
            cboHourEndInternal.Text = "23"
            cboMinStartInternal.Text = "00"
            cboMinEndInternal.Text = "50"

            '팀명
            temp = "SELECT '' S_MENU_CD, '-' S_MENU_NM UNION " & _
                    "SELECT S_MENU_CD, S_MENU_NM FROM t_s_code Where COM_CD='" & gsCOM_CD & "' and L_MENU_CD='010' Order By S_MENU_CD "
            CB_Set(gsConString, temp, cboTeam, "S_MENU_NM", "S_MENU_CD", gsTeam_CD)
            '구분
            temp = "SELECT '0' S_MENU_CD, '일정관리' S_MENU_NM UNION " & _
                    "SELECT '1', '외근관리' Order By S_MENU_CD "
            CB_Set(gsConString, temp, drpGubun, "S_MENU_NM", "S_MENU_CD", "0")
            '외근자
            temp = "SELECT '' USER_ID, '-' USER_NM UNION " & _
                    "SELECT USER_ID, Concat(USER_ID,'.',USER_NM) USER_NM FROM t_user Where COM_CD='" & gsCOM_CD & "' AND (RETIRE_DD IS NULL OR RTRIM(RETIRE_DD)='') Order By USER_ID "
            CB_Set(gsConString, temp, cboUserCustomer, "USER_NM", "USER_ID", "")
            '외근사유
            temp = "SELECT '' S_MENU_CD, '-' S_MENU_NM UNION " & _
                    "SELECT S_MENU_CD, S_MENU_NM FROM t_s_code Where COM_CD='" & gsCOM_CD & "' and L_MENU_CD='007' Order By S_MENU_CD "
            CB_Set(gsConString, temp, cboWReasonCustomer, "S_MENU_NM", "S_MENU_CD", "")


            '달력에 있는 라벨 클릭 이벤트에 Day_Click 함수 연결
            Dim obj As Label = New Label
            For Each ctrl In FlowLayoutPanel1.Controls
                If ctrl.GetType() Is obj.GetType Then
                    obj = ctrl
                    AddHandler obj.Click, AddressOf Day_Click
                End If
            Next

        Catch ex As Exception
            Call WriteLog(Me.Name & " : " & ex.ToString)
        Finally
            dt = Nothing
        End Try
    End Sub

    Private Sub UserList_Setting()
        Dim dt As DataTable
        Dim i As Short
        Dim item1 As DataRowView
        Dim Team_cd As String = ""
        Try
            If cboTeam.SelectedIndex > -1 AndAlso cboTeam.SelectedValue.ToString.Contains("System.") = False Then
                Team_cd = cboTeam.SelectedValue.ToString()
            End If

            temp = "SELECT USER_ID, Concat(USER_ID,'.',USER_NM) USER_NM, DEPART_CD, DEPART_NM " & _
                   "From t_user a " & _
                   "Where COM_CD='" & gsCOM_CD & _
                   " AND TEAM_CD like '" & Team_cd & "%' AND (RETIRE_DD IS NULL OR RTRIM(RETIRE_DD)='') "
            CB_Set(gsConString, temp, lboTeamUserTemp, "USER_NM", "USER_ID", "")

            lboTeamUserList.BeginUpdate()
            lboTeamUserList.Items.Clear()
            For i = 0 To lboTeamUserTemp.Items.Count - 1
                item1 = lboTeamUserTemp.Items(i)
                If lboTeamUserSelected.FindString(item1.Row(1).ToString) < 0 Then _
                    lboTeamUserList.Items.Add(item1.Row(1).ToString) 'item1.Row(0) : ValueMember, item1.Row(1) : DisplayMember
            Next
            lboTeamUserList.EndUpdate()

        Catch ex As Exception
            Call WriteLog(Me.Name & " : " & ex.ToString)
        Finally
            dt = Nothing
        End Try
    End Sub

    Private Sub Calendar_Setting()
        Try
            Dim obj As Label = New Label
            Dim i As Short = 0
            Dim j As Short = 0
            Dim cur_month As Integer = DPMonth.Value.Month
            Dim startday As Date = New Date(DPYear.Value.Year, DPMonth.Value.Month, 1)
            Dim startdayofweek As Short = CShort(startday.DayOfWeek)
            'startday.DayOfWeek :1(sun:0, sat:6)   startday.DayOfWeek.ToString : Monday 

            For Each ctrl In FlowLayoutPanel1.Controls
                If ctrl.GetType() Is obj.GetType Then
                    obj = ctrl
                    obj.Image = Nothing
                    obj.BackColor = Color.White
                    obj.BorderStyle = BorderStyle.None
                    'Call WriteLog(Me.Name & " : j=" & j.ToString & " startdayofweek=" & startdayofweek.ToString & " i=" & i.ToString & " startday.AddDays(i).Month=" & startday.AddDays(i).Month.ToString & " DTMonth=" & cur_month)
                    If j < 7 AndAlso j < startdayofweek Then
                        obj.Text = ""
                        obj.Enabled = False
                    Else
                        obj.Enabled = Enabled
                        If startday.AddDays(i).Month = cur_month Then
                            i += 1
                            '날짜 지정
                            obj.Text = i.ToString
                            '요일별 폰트 색상 지정
                            Select Case startday.AddDays(i - 1).DayOfWeek
                                Case DayOfWeek.Sunday
                                    obj.ForeColor = Color.Red
                                Case DayOfWeek.Saturday
                                    obj.ForeColor = Color.Blue
                                Case Else
                                    obj.ForeColor = Color.Black
                            End Select
                            '오늘 표시
                            If startday.AddDays(i - 1) = Today Then
                                obj.BorderStyle = BorderStyle.FixedSingle
                            End If
                        Else
                            obj.Text = ""
                            obj.Enabled = False
                        End If
                    End If

                    j += 1
                    'Call WriteLog(Me.Name & " : " & obj.Name & "-" & obj.Text & "-" & obj.Tag)
                End If
            Next

            Call Schedule_Get()

        Catch ex As Exception
            Call WriteLog(Me.Name & " : " & ex.ToString)
        Finally

        End Try
    End Sub

    Private Sub Schedule_Get()
        Dim dt As DataTable
        Dim i As Short
        Dim obj As Label = New Label
        Dim tmpDate As String
        'Dim tmpPath As String = Application.StartupPath & "\resources\mark1.bmp"
        'Call WriteLog(Me.Name & " : tmpPath=" & tmpPath)
        Try
            temp = "SELECT SUBSTRING(S_START_TIME,1,8) S_START_DD " & _
                   " ,SUBSTRING(S_END_TIME,1,8) S_END_DD " & _
                   " ,if(SHARING_TYPE = 'S',1,0) SHARING " & _
                   " ,SHARING_TYPE " & _
                   " FROM t_schedule  " & _
                   " WHERE COM_CD='" & gsCOM_CD & _
                   "' AND S_START_TIME <= '" & DPYear.Text & DPMonth.Text & "320000'" & _
                   " AND S_END_TIME >= '" & DPYear.Text & DPMonth.Text & "000000'" & _
                   " AND ((SHARING_TYPE = 'P' AND REGISTRANT LIKE '" & gsUSER_ID.Trim & ".%')" & _
                   "      OR SHARING_TYPE = 'S' OR SHARING_TYPE = 'O')" & _
                   " ORDER BY S_START_TIME, S_END_TIME "
            dt = MiniCTI.DoQueryNoErrorCatch(gsConString, temp)

            For i = 0 To dt.Rows.Count - 1
                For Each ctrl In FlowLayoutPanel1.Controls
                    If ctrl.GetType() Is obj.GetType Then
                        obj = ctrl
                        tmpDate = DPYear.Text & DPMonth.Text & If(obj.Text.Length = 2, obj.Text, "0" & obj.Text)
                        tmpDate = If(tmpDate.Length = 8, tmpDate, "")
                        If obj.Text.Trim <> "" AndAlso _
                            dt.Rows(i).Item(0).ToString <= tmpDate AndAlso dt.Rows(i).Item(1).ToString >= tmpDate Then
                            'obj.Image = Image.FromFile(tmpPath)
                            ' obj.Image = PictureBox2.Image  '20*20
                            obj.Image = PictureBox2.Image  '15*15
                        End If
                    End If
                Next
            Next

        Catch ex As Exception
            Call WriteLog(Me.Name & " : " & ex.ToString)
        Finally
            dt = Nothing
        End Try
    End Sub

    Private Sub Day_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim obj As Label = CType(sender, Label)
            Dim obj2 As Label = New Label
            For Each ctrl In FlowLayoutPanel1.Controls
                If ctrl.GetType() Is obj2.GetType Then
                    obj2 = ctrl
                    obj2.BackColor = Color.White
                End If
            Next

            sel_day = obj
            'If obj.Text.Trim.Length < 1 Then
            '    Schedule_Init(0)
            '    Exit Try
            'End If

            obj.BackColor = Color.WhiteSmoke

            temp = "SELECT Concat(SUBSTRING(S_START_TIME,1,4),'-',SUBSTRING(S_START_TIME,5,2),'-',SUBSTRING(S_START_TIME,7,2),' ',SUBSTRING(S_START_TIME,9,2),':',SUBSTRING(S_START_TIME,11,2)) S_START_TIME " & _
                   " ,Concat(SUBSTRING(S_END_TIME,1,4),'-',SUBSTRING(S_END_TIME,5,2),'-',SUBSTRING(S_END_TIME,7,2),' ',SUBSTRING(S_END_TIME,9,2),':',SUBSTRING(S_END_TIME,11,2)) S_END_TIME " & _
                   " ,if(SHARING_TYPE = 'P',0,1) SHARING, REGISTRANT, SHARING_TYPE, if(SHARING_TYPE = 'O','외근관리','일정관리') SHARING_TYPE2, S_TITLE, S_DESC, S_COMPANY_COWORKER, S_WORKOUT_REASON, S_WORKOUT_LOC " & _
                   " FROM t_schedule  " & _
                   " WHERE COM_CD='" & gsCOM_CD & _
                   "' AND S_START_TIME <= '" & DPYear.Text & DPMonth.Text & If(obj.Text.Length = 1, "0" & obj.Text, obj.Text) & "2400'" & _
                   " AND S_END_TIME >= '" & DPYear.Text & DPMonth.Text & If(obj.Text.Length = 1, "0" & obj.Text, obj.Text) & "0000'" & _
                   " AND ((SHARING_TYPE = 'P' AND REGISTRANT LIKE '" & gsUSER_ID.Trim & ".%')" & _
                   "      OR SHARING_TYPE = 'S' OR SHARING_TYPE = 'O')" & _
                   " ORDER BY S_START_TIME, S_END_TIME, SHARING_TYPE, REGISTRANT "
            GV_DataBind(gsConString, temp, DataGridView1)
            Schedule_Init(1)
        Catch ex As Exception
            Call WriteLog(Me.Name & " : " & ex.ToString)
        Finally

        End Try
    End Sub

    Private Sub DataGridView1_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Try
            If e.RowIndex < 0 Then Exit Try
            Dim i As Integer = e.RowIndex
            Dim info As String = ""
            'With DataGridView1.Rows(i)
            '    info = .Cells("S_START_TIME").Value.ToString & "^" & .Cells("S_END_TIME").Value.ToString & "^" & .Cells("SHARING").Value.ToString & _
            '    "^" & .Cells("REGISTRANT").Value.ToString & "^" & .Cells("S_TITLE").Value.ToString & "^" & .Cells("SHARING_TYPE").Value.ToString & _
            '    "^" & .Cells("S_COMPANY_COWORKER").Value.ToString & "^" & .Cells("S_DESC").Value.ToString & _
            '    "^" & .Cells("SHARING_TYPE2").Value.ToString & "^" & .Cells("S_WORKOUT_REASON").Value.ToString & "^" & .Cells("S_WORKOUT_LOC").Value.ToString
            'End With
            Schedule_Setting(i)
        Catch ex As Exception
            Call WriteLog(Me.Name & " : " & ex.ToString)
        End Try
    End Sub


    Private Sub Schedule_Setting(ByVal k As Short)
        Dim dt As DataTable
        Try
            If k < 0 Then Exit Try
            'DataGridView1 컬럼 : S_START_TIME, S_END_TIME, SHARING, SHARING_TYPE2, REGISTRANT, S_TITLE, SHARING_TYPE, S_DESC, S_COMPANY_COWORKER, S_WORKOUT_REASON, S_WORKOUT_LOC
            '데이타 포맷: 201108051900  201108060000    0   일정관리    0001.개똥이	전체회식1   P	'전체' "회식1" & ....	0001,0002,0003,0004             
            With DataGridView1.Rows(k)
                drpGubun.SelectedValue = If(.Cells("SHARING_TYPE").Value.ToString.Trim = "O", "1", "0")  ' 0=>외근, S/P=>일정
                dtpStartInternal.Text = .Cells("S_START_TIME").Value.ToString.Trim.Split(" ")(0)
                cboHourStartInternal.Text = .Cells("S_START_TIME").Value.ToString.Trim.Split(" ")(1).Split(":")(0)
                cboMinStartInternal.Text = .Cells("S_START_TIME").Value.ToString.Trim.Split(" ")(1).Split(":")(1)
                dtpEndInternal.Text = .Cells("S_END_TIME").Value.ToString.Trim.Split(" ")(0)
                cboHourEndInternal.Text = .Cells("S_END_TIME").Value.ToString.Trim.Split(" ")(1).Split(":")(0)
                cboMinEndInternal.Text = .Cells("S_END_TIME").Value.ToString.Trim.Split(" ")(1).Split(":")(1)
                ckbSharing.Checked = .Cells("SHARING").Value
                txtTitleInternal.Text = .Cells("S_TITLE").Value.ToString
                txtDescInternal.Text = .Cells("S_DESC").Value.ToString.Trim
                cboTeam.SelectedValue = ""
                lboTeamUserList.Items.Clear()
                lboTeamUserSelected.Items.Clear()

                Call UserList_Setting()
                'Call WriteLog("Schedule_Setting>>>  drpGubun:" & drpGubun.SelectedValue & " S_COMPANY_COWORKER:" & .Cells("S_COMPANY_COWORKER").Value.ToString.Trim)
                '일정관리
                '20120120 ' If .Cells("S_COMPANY_COWORKER").Value.ToString.Trim <> "" Then
                If drpGubun.SelectedValue = "0" And .Cells("S_COMPANY_COWORKER").Value.ToString.Trim <> "" Then
                    Dim str As String() = .Cells("S_COMPANY_COWORKER").Value.ToString.Split(",")
                    Dim i, j As Short
                    lboTeamUserSelected.BeginUpdate()
                    For i = 0 To str.Length - 1
                        j = lboTeamUserTemp.FindString(str(i) & ".")
                        If j > -1 Then
                            lboTeamUserSelected.Items.Add(lboTeamUserTemp.GetItemText(lboTeamUserTemp.Items(j)))
                        End If
                    Next
                    lboTeamUserSelected.EndUpdate()

                    If lboTeamUserList.Items.Count > 0 AndAlso lboTeamUserSelected.Items.Count > 0 Then
                        lboTeamUserList.BeginUpdate()
                        For i = 0 To lboTeamUserSelected.Items.Count - 1
                            If lboTeamUserList.Items.Contains(lboTeamUserSelected.Items(i).ToString) Then
                                lboTeamUserList.Items.Remove(lboTeamUserSelected.Items(i).ToString)
                            End If
                        Next
                        lboTeamUserList.EndUpdate()
                    End If

                End If
                '외근관리
                If drpGubun.SelectedValue = "1" And .Cells("S_COMPANY_COWORKER").Value.ToString.Trim <> "" Then
                    cboUserCustomer.SelectedValue = .Cells("S_COMPANY_COWORKER").Value.ToString.Trim.Split(".")(0)
                End If
                cboWReasonCustomer.SelectedValue = .Cells("S_WORKOUT_REASON").Value.ToString.Trim
                txtWLocCustomer.Text = .Cells("S_WORKOUT_LOC").Value.ToString.Trim

            End With
        Catch ex As Exception
            Call WriteLog(Me.Name & " : " & ex.ToString)
        Finally
            dt = Nothing
        End Try
    End Sub

    Private Sub Schedule_Init(ByVal Gubun As Short)
        Try
            If Gubun = 0 Then DataGridView1.DataSource = Nothing
            dtpStartInternal.Value = Now
            dtpEndInternal.Value = Now
            cboHourStartInternal.Text = "00"
            cboHourEndInternal.Text = "23"
            cboMinStartInternal.Text = "00"
            cboMinEndInternal.Text = "50"
            ckbSharing.Checked = False
            txtTitleInternal.Text = ""
            txtDescInternal.Text = ""
            cboTeam.SelectedValue = ""
            'drpTeam.SelectedValue = gsTeam_CD
            lboTeamUserList.Items.Clear()
            lboTeamUserSelected.Items.Clear()
            txtWLocCustomer.Text = ""
            Call UserList_Setting()
            drpGubun.SelectedValue = "0"
            cboUserCustomer.SelectedValue = ""
            cboWReasonCustomer.SelectedValue = ""
        Catch ex As Exception
            Call WriteLog(Me.Name & " : " & ex.ToString)
        End Try
    End Sub

    Private Sub DTYear_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DPYear.ValueChanged
        Try
            If temp_Date = DPYear.Text & DPMonth.Text Then Exit Sub
            temp_Date = DPYear.Text & DPMonth.Text
            Calendar_Setting()
            Schedule_Init(0)
        Catch ex As Exception
            Call WriteLog(Me.Name & " : " & ex.ToString)
        End Try
    End Sub

    Private Sub DTMonth_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DPMonth.ValueChanged
        Try
            If temp_Date = DPYear.Text & DPMonth.Text Then Exit Sub
            temp_Date = DPYear.Text & DPMonth.Text
            Calendar_Setting()
            Schedule_Init(0)
        Catch ex As Exception
            Call WriteLog(Me.Name & " : " & ex.ToString)
        End Try
    End Sub

    Private Sub btn_today_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_today.Click
        Try
            If DPYear.Value.Year <> Today.Year Then DPYear.Value = Today
            If DPMonth.Value.Month <> Today.Month Then DPMonth.Value = Today
        Catch ex As Exception
            Call WriteLog(Me.Name & " : " & ex.ToString)
        End Try
    End Sub


    Private Sub drpTeam_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTeam.SelectedIndexChanged
        Try
            UserList_Setting()
        Catch ex As Exception
            Call WriteLog(Me.Name & " : " & ex.ToString)
        End Try
    End Sub

    Private Sub drpGubun_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Select Case drpGubun.SelectedValue.ToString
                Case "1"   '외근관리
                    cboUserCustomer.Enabled = True
                    cboWReasonCustomer.Enabled = True
                    txtWLocCustomer.Enabled = True
                    ckbSharing.Enabled = False
                    cboTeam.Enabled = False
                    lboTeamUserList.Enabled = False
                    lboTeamUserSelected.Enabled = False
                    btnAdd.Enabled = False
                    btnRemove.Enabled = False
                    btnAddAll.Enabled = False
                    btnRemoveAll.Enabled = False
                Case Else
                    cboUserCustomer.Enabled = False
                    cboWReasonCustomer.Enabled = False
                    txtWLocCustomer.Enabled = False
                    ckbSharing.Enabled = True
                    cboTeam.Enabled = True
                    lboTeamUserList.Enabled = True
                    lboTeamUserSelected.Enabled = True
                    btnAdd.Enabled = True
                    btnRemove.Enabled = True
                    btnAddAll.Enabled = True
                    btnRemoveAll.Enabled = True
            End Select
        Catch ex As Exception
            Call WriteLog(Me.Name & " : " & ex.ToString)
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            If lboTeamUserList.SelectedIndex > -1 Then
                If lboTeamUserSelected.Items.Contains(lboTeamUserList.SelectedItem) = False Then lboTeamUserSelected.Items.Add(lboTeamUserList.SelectedItem)
                lboTeamUserList.Items.Remove(lboTeamUserList.SelectedItem)
                lboTeamUserList.ClearSelected()
                lboTeamUserSelected.ClearSelected()
            End If
        Catch ex As Exception
            Call WriteLog(Me.Name & " : " & ex.ToString)
        Finally

        End Try
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Try
            If lboTeamUserSelected.SelectedIndex > -1 Then
                If lboTeamUserTemp.FindString(lboTeamUserSelected.SelectedItem.ToString) >= 0 AndAlso lboTeamUserList.Items.Contains(lboTeamUserSelected.SelectedItem) = False Then lboTeamUserList.Items.Add(lboTeamUserSelected.SelectedItem)
                lboTeamUserSelected.Items.Remove(lboTeamUserSelected.SelectedItem)
                lboTeamUserList.ClearSelected()
                lboTeamUserSelected.ClearSelected()
            End If
        Catch ex As Exception
            Call WriteLog(Me.Name & " : " & ex.ToString)
        Finally

        End Try
    End Sub

    Private Sub btnAdd2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddAll.Click
        Try
            Dim i As Integer
            For i = 0 To lboTeamUserList.Items.Count - 1
                lboTeamUserSelected.Items.Add(lboTeamUserList.Items(i))
            Next
            lboTeamUserList.Items.Clear()
            lboTeamUserList.ClearSelected()
            lboTeamUserSelected.ClearSelected()
        Catch ex As Exception
            Call WriteLog(Me.Name & " : " & ex.ToString)
        Finally

        End Try
    End Sub

    Private Sub btnRemove2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveAll.Click
        Try
            Dim i As Integer
            For i = 0 To lboTeamUserSelected.Items.Count - 1
                lboTeamUserList.Items.Add(lboTeamUserSelected.Items(i))
            Next
            lboTeamUserSelected.Items.Clear()
            lboTeamUserList.ClearSelected()
            lboTeamUserSelected.ClearSelected()
        Catch ex As Exception
            Call WriteLog(Me.Name & " : " & ex.ToString)
        Finally

        End Try
    End Sub

    Public Sub gsSave()
        Dim dt As DataTable
        Dim i As Short
        Dim users As String = ""
        Try
            If txtTitleInternal.Text.Trim = "" Then
                MsgBox("제목을 입력하십시오.", MsgBoxStyle.OkOnly, "정보")
                Exit Try
            ElseIf cboHourStartInternal.Text.Trim = "" OrElse cboMinStartInternal.Text.Trim = "" Then
                MsgBox("시작시간을 선택하십시오.", MsgBoxStyle.OkOnly, "정보")
                Exit Try
            ElseIf cboHourEndInternal.Text.Trim = "" OrElse cboMinEndInternal.Text.Trim = "" Then
                MsgBox("종료시간을 선택하십시오.", MsgBoxStyle.OkOnly, "정보")
                Exit Try
            ElseIf dtpStartInternal.Text.Replace("-", "").Replace("/", "") & cboHourStartInternal.Text.Trim & cboMinStartInternal.Text.Trim > dtpEndInternal.Text.Replace("-", "").Replace("/", "") & cboHourEndInternal.Text.Trim & cboMinEndInternal.Text.Trim Then
                MsgBox("종료일시는 시작일시 이후로 선택하십시오.", MsgBoxStyle.OkOnly, "정보")
                Exit Try
            ElseIf drpGubun.SelectedValue.ToString.Trim = "1" And drpUser.SelectedValue.ToString = "" Then
                MsgBox("외근자를 선택하십시오.", MsgBoxStyle.OkOnly, "정보")
                Exit Try
            ElseIf drpGubun.SelectedValue.ToString.Trim = "0" Then
                cboUserCustomer.SelectedValue = ""
                cboWReasonCustomer.SelectedValue = ""
                txtWLocCustomer.Text = ""
            End If



            temp = "SELECT count(S_START_TIME) From t_schedule  "
            '20120120
            If drpGubun.SelectedValue = "0" Then   '일정관리
                temp2 = " Where COM_CD='" & gsCOM_CD & _
                       "' AND S_START_TIME='" & DPDate1.Text.Replace("-", "").Replace("/", "") & drpHour1.Text & drpMin1.Text & _
                       "' AND REGISTRANT LIKE '" & gsUSER_ID.Trim & ".%" & _
                       "' AND SHARING_TYPE='" & If(drpGubun.SelectedValue.ToString.Trim = "1", "O", If(chkSharing.Checked = False, "P", "S")) & _
                       "' AND S_TITLE='" & txtTitle.Text.Trim & "' "

                For i = 0 To lboTeamUserSelected.Items.Count - 1
                    users &= lboTeamUserSelected.Items(i).ToString.Split(".")(0) & ","
                Next
                If users.Trim.Length > 0 Then users = users.Substring(0, users.Trim.Length - 1)

            Else '외근관리
                temp2 = " Where COM_CD='" & gsCOM_CD & _
                       "' AND S_START_TIME='" & DPDate1.Text.Replace("-", "").Replace("/", "") & drpHour1.Text & drpMin1.Text & _
                       "' AND S_COMPANY_COWORKER LIKE '" & drpUser.SelectedValue.ToString.Trim & ".%" & _
                       "' AND SHARING_TYPE='" & If(drpGubun.SelectedValue.ToString.Trim = "1", "O", If(chkSharing.Checked = False, "P", "S")) & "' "

                users = cboUserCustomer.Text
                'Call WriteLog("drpUser.Text:" & drpUser.Text & "  drpUser.SelectedValue:" & drpUser.SelectedValue & "  drpUser.SelectedText:" & drpUser.SelectedText & "  drpUser.SelectedItem:" & drpUser.SelectedItem.ToString)
            End If

            dt = MiniCTI.DoQueryNoErrorCatch(gsConString, temp & temp2)

            If dt.Rows(0).Item(0).ToString.Trim = "0" Then
                temp = "Insert into t_schedule(COM_CD,S_START_TIME,S_END_TIME,REGISTRANT,SHARING_TYPE,S_TITLE,S_COMPANY_COWORKER,S_DESC,S_WORKOUT_REASON,S_WORKOUT_LOC) values('" & _
                        gsCOM_CD & "','" & DPDate1.Text.Replace("-", "").Replace("/", "") & drpHour1.Text & drpMin1.Text & "','" & _
                        DPDate2.Text.Replace("-", "").Replace("/", "") & drpHour2.Text & drpMin2.Text & "','" & _
                        gsUSER_ID.Trim & "." & gsUSER_NM.Trim & "','" & If(drpGubun.SelectedValue = "1", "O", If(chkSharing.Checked = False, "P", "S")) & "','" & _
                        txtTitle.Text.Trim & "','" & users & "','" & txtSchedule.Text.Trim.Replace("'", "''") & "','" & drpWReason.SelectedValue.ToString.Trim & "','" & txtWLoc.Text.Trim & "') "

                temp2 = ""
            Else
                temp = "Update t_schedule set S_END_TIME='" & dtpEndInternal.Text.Replace("-", "").Replace("/", "") & cboHourEndInternal.Text & cboMinEndInternal.Text & "' " & _
                        ",S_COMPANY_COWORKER='" & users & "',S_DESC='" & txtDescInternal.Text.Trim.Replace("'", "''") & "',S_WORKOUT_REASON='" & cboWReasonCustomer.SelectedValue.ToString.Trim & "',S_WORKOUT_LOC='" & txtWLocCustomer.Text.Trim & "' "
            End If
            dt.Reset()

            dt = MiniCTI.DoQueryParam(gsConString, temp & temp2)
            MsgBox("처리되었습니다.", MsgBoxStyle.OkOnly, "정보")
            Call Calendar_Setting()
            Day_Click(sel_day, Nothing)
            Schedule_Init(1)
        Catch ex As Exception
            Call WriteLog(Me.Name & " : " & ex.ToString)
        Finally
            dt = Nothing
        End Try
    End Sub

    Public Sub gsDelete()
        Dim dt As DataTable
        Try
            If txtTitleInternal.Text.Trim = "" Then
                MsgBox("삭제할 일정을 다시 선택하십시오.", MsgBoxStyle.OkOnly, "정보")
                Exit Try
            End If
            temp = "SELECT count(S_START_TIME) From t_schedule "
            If drpGubun.SelectedValue = "0" Then   '일정관리
                temp2 = " Where COM_CD='" & gsCOM_CD & _
                       "' AND S_START_TIME='" & DPDate1.Text.Replace("-", "").Replace("/", "") & drpHour1.Text & drpMin1.Text & _
                       "' AND REGISTRANT LIKE '" & gsUSER_ID.Trim & ".%" & _
                       "' AND SHARING_TYPE='" & If(drpGubun.SelectedValue.ToString.Trim = "1", "O", If(chkSharing.Checked = False, "P", "S")) & _
                       "' AND S_TITLE='" & txtTitle.Text.Trim & "' "

            Else '외근관리
                temp2 = " Where COM_CD='" & gsCOM_CD & _
                       "' AND S_START_TIME='" & DPDate1.Text.Replace("-", "").Replace("/", "") & drpHour1.Text & drpMin1.Text & _
                       "' AND S_COMPANY_COWORKER LIKE '" & drpUser.SelectedValue.ToString.Trim & ".%" & _
                       "' AND SHARING_TYPE='" & If(drpGubun.SelectedValue.ToString.Trim = "1", "O", If(chkSharing.Checked = False, "P", "S")) & "' "
            End If

            dt = MiniCTI.DoQueryNoErrorCatch(gsConString, temp & temp2)

            If dt.Rows(0).Item(0).ToString.Trim = "0" Then
                MsgBox("삭제할 일정을 다시 선택하십시오.", MsgBoxStyle.OkOnly, "정보")
                Exit Try
            End If
            dt.Reset()
            MsgBox(txtTitleInternal.Text.Trim & " 일정을 삭제하시겠습니까?", MsgBoxStyle.YesNo, "확인")

            temp = "Delete from t_schedule "
            dt = MiniCTI.DoQueryParam(gsConString, temp & temp2)
            MsgBox("처리되었습니다.", MsgBoxStyle.OkOnly, "정보")
            Call Calendar_Setting()
            Day_Click(sel_day, Nothing)
            Schedule_Init(1)
        Catch ex As Exception
            Call WriteLog(Me.Name & " : " & ex.ToString)
        Finally
            dt = Nothing
        End Try
    End Sub

    Public Sub gsFormExit()
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Call gsSave()
    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        Call gsDelete()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click

    End Sub
End Class