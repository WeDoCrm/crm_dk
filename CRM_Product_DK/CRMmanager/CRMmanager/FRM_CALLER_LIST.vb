Public Class FRM_CALLER_LIST
    Private ss As New CRMmanager
    Private result As Integer = 0

    Private Sub FRM_CALLER_LIST_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Call SettoolBar(False, True, False, False, False, True, True)
    End Sub

    Private Sub FRM_CALLER_LIST_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Call SettoolBar(False, True, False, False, False, True, True)

            dpt1.Value = Format(Now, "yyyy-MM-dd")
            dpt2.Value = Format(Now, "yyyy-MM-dd")

            If Not gbUseExcel Then
                btnExcel.Visible = False
                btnSelect.Left = btnExcel.Left
            End If

            cbH1.SelectedIndex = 0
            cbT1.SelectedIndex = 0

            cbH2.SelectedIndex = 23
            cbT2.SelectedIndex = 6

            '************************************** 통화자 *********************************************
            Dim SQL As String = " SELECT '' ,'XXXX' UNION ALL SELECT LTRIM(RTRIM(USER_NM)), CONCAT(USER_ID,'.',LTRIM(RTRIM(USER_NM))) FROM T_USER WHERE COM_CD = '" & gsCOM_CD & "'"
            Dim dt4 As DataTable = DoQueryNoErrorCatch(gsConString, SQL)

            cboUser.DataSource = dt4
            cboUser.DisplayMember = dt4.Columns(0).ToString
            cboUser.ValueMember = dt4.Columns(1).ToString

            cboCallResult.SelectedIndex = 0
            cboUser.SelectedIndex = 0
            dt4 = Nothing

            Call gsSelect(True)

        Catch ex As Exception
            Call WriteLog("FRM_HISTORY : " & ex.ToString)
        End Try


    End Sub

    Public Function genQuery(ByVal isInit As Boolean) As String

        '        Select b.ani
        '		,a.CUSTOMER_NM 
        '		,b.max_start_time 
        '      ,CONCAT(SUBSTRING(b.max_start_time,1,4) , '-' , SUBSTRING(b.max_start_time,5,2) , '-' , SUBSTRING(b.max_start_time,7,2)) tong_dd
        '      ,CONCAT(SUBSTRING(b.max_start_time,9,2) , ':' , SUBSTRING(b.max_start_time,11,2), ':' , SUBSTRING(b.max_start_time,13,2)) tong_time
        '      ,b.TONG_USER
        '      ,case when b.call_result in ('1', '4') then '부재중'
        '            when b.call_result in ('3','5') then '수신'
        '            when b.call_result = '2' then '발신' end call_result2
        '		,CONCAT(b.call_result, '.' , (SELECT LTRIM(RTRIM(S_MENU_NM)) FROM T_S_CODE WHERE COM_CD = '9997' AND L_MENU_CD = '009' AND S_MENU_CD = b.call_result )) call_result
        '		,CONCAT(a.CALL_TYPE , '.' , (SELECT LTRIM(RTRIM(S_MENU_NM)) FROM T_S_CODE WHERE COM_CD = '9997' AND L_MENU_CD = '005' AND S_MENU_CD = a.CALL_TYPE )) CALL_TYPE
        '      ,CONCAT(a.CONSULT_RESULT , '.' , (SELECT LTRIM(RTRIM(S_MENU_NM)) FROM T_S_CODE WHERE COM_CD = '9997' AND L_MENU_CD = '004' AND S_MENU_CD = a.CONSULT_RESULT )) CONSULT_RESULT
        '      ,CONCAT(a.CONSULT_TYPE , '.' , (SELECT LTRIM(RTRIM(S_MENU_NM)) FROM T_S_CODE WHERE COM_CD = '9997' AND L_MENU_CD = '003' AND S_MENU_CD = a.CONSULT_TYPE )) CONSULT_TYPE
        '      ,a.TONG_CONTENTS
        '  from
        '            (
        '		select s1.*
        '                from
        '              (
        '              select r1.call_id, r1.ani, r1.max_start_time, r1.call_result, r1.tong_user
        '                from (
        '                  select y1.call_id, y1.ani, y1.max_start_time, y1.call_result, y1.tong_user,
        '                        case when (next_call_result = 'N' and call_result = 1) then 'N' else 'Y' end needKeep
        '                    from (		
        '                      select w1.call_id, w1.ani, w1.max_start_time, w1.call_result, w1.tong_user,
        '                            case (select min(call_result) from t_call_history u1
        '                                  where(w1.ani = u1.ani)
        '                                  and w1.call_result = 1
        '                                  and w1.next_start_time = u1.TONG_START_TIME) when 3 then 'N' when 4 then 'N' when 5 then 'N' else 'Y' end next_call_result
        '                        from (   
        '                           select t1.call_id, t1.ani, t1.max_start_time, t2.call_result, t2.tong_user,
        '                                   (select min(max_start_time) from 
        '                                          (select call_id, ani, max(tong_start_time) max_start_time from t_call_history
        '                                          group by call_id, ani) sub1
        '                                       where sub1.max_start_time > t1.max_start_time
        '                                         and sub1.ani = t1.ani) next_start_time  
        '                              from
        '                                (select call_id, ani, max(tong_start_time) max_start_time, max(call_result) call_result from t_call_history
        '                                where COM_CD = '0001'
        '                                  and tong_start_time >= '20120712240000'
        '                                  and tong_start_time < '20120718240000'
        '                                  and ani like '%'
        '                                  and tong_user like '%'

        '                                group by call_id, ani) t1,
        '                                t_call_history t2
        '                            where
        '                              and t2.COM_CD = '0001'
        '                              and t2.tong_start_time >= '20120712240000'
        '                              and t2.tong_start_time < '20120719240000'
        '                              and t2.ani like '%'
        '                              and t2.tong_user like '%'
        '                              and t1.call_id = t2.call_id
        '                              and t1.ani = t2.ani
        '                              and t1.max_start_time = t2.tong_start_time
        '                              and t1.call_result = t2.call_result
        '                              order by max_start_time
        '                              ) w1
        '                            ) y1 
        '                             order by max_start_time
        '                ) r1
        '                where r1.needKeep = 'Y'              
        '                 -- order by max_start_time 
        '               union all
        '                select tong_telno call_id, tong_telno ani,CONCAT(tond_dd, tong_time) max_start_time,
        '                                          '2' call_result, tong_user
        '                  from t_customer_history
        '                where call_type = 2
        '                  and COM_CD = '0001'
        '                  and tond_dd >= '20120712'
        '                  and tond_dd < '20120719'
        '                  and tong_time >= '000000'
        '                  and tong_time < '240000'
        '                  and tong_telno like '%'
        '                  and tong_user like '%'
        '                ) s1
        '            ) b left join t_customer_history a
        '     on a.tong_telno = b.ani
        '    and CONCAT(a.tond_dd, a.tong_time) = b.max_start_time
        'order by max_start_time

        Dim SQL As String = " Select b.ani"
        SQL = SQL & "		,a.CUSTOMER_NM  "
        'SQL = SQL & "		,b.max_start_time  "
        SQL = SQL & "      ,CONCAT(SUBSTRING(b.max_start_time,1,4) , '-' , SUBSTRING(b.max_start_time,5,2) , '-' , SUBSTRING(b.max_start_time,7,2)) tong_dd"
        SQL = SQL & "      ,CONCAT(SUBSTRING(b.max_start_time,9,2) , ':' , SUBSTRING(b.max_start_time,11,2), ':' , SUBSTRING(b.max_start_time,13,2)) tong_time"
        SQL = SQL & "      ,b.TONG_USER"
        SQL = SQL & "      ,case when b.call_result in ('1', '4') then '부재중'"
        SQL = SQL & "            when b.call_result in ('3','5') then '수신'"
        SQL = SQL & "            when b.call_result = '2' then '발신' end call_result2"
        'SQL = SQL & "		,CONCAT(b.call_result, '.' , (SELECT LTRIM(RTRIM(S_MENU_NM)) FROM T_S_CODE WHERE COM_CD = '" & gsCOM_CD & "' AND L_MENU_CD = '009' AND S_MENU_CD = b.call_result )) call_result"
        SQL = SQL & "		,CONCAT(a.CALL_TYPE , '.' , (SELECT LTRIM(RTRIM(S_MENU_NM)) FROM T_S_CODE WHERE COM_CD = '" & gsCOM_CD & "' AND L_MENU_CD = '005' AND S_MENU_CD = a.CALL_TYPE )) CALL_TYPE"
        SQL = SQL & "      ,CONCAT(a.CONSULT_RESULT , '.' , (SELECT LTRIM(RTRIM(S_MENU_NM)) FROM T_S_CODE WHERE COM_CD ='" & gsCOM_CD & "'AND L_MENU_CD = '004' AND S_MENU_CD = a.CONSULT_RESULT )) CONSULT_RESULT"
        'SQL = SQL & "      ,CONCAT(a.CONSULT_TYPE , '.' , (SELECT LTRIM(RTRIM(S_MENU_NM)) FROM T_S_CODE WHERE COM_CD ='" & gsCOM_CD & "'AND L_MENU_CD = '003' AND S_MENU_CD = a.CONSULT_TYPE )) CONSULT_TYPE"
        'SQL = SQL & "      ,a.TONG_CONTENTS"
        SQL = SQL & "  from"
        SQL = SQL & "            ("
        SQL = SQL & "			select s1.*"
        SQL = SQL & "                from"
        SQL = SQL & "              ("
        SQL = SQL & "              select r1.call_id, r1.ani, r1.max_start_time, r1.call_result, r1.tong_user"
        SQL = SQL & "                from ("
        SQL = SQL & "                  select y1.call_id, y1.ani, y1.max_start_time, y1.call_result, y1.tong_user,"
        SQL = SQL & "                        case when (next_call_result = 'N' and call_result = 1) then 'N' else 'Y' end needKeep"
        SQL = SQL & "                    from (		"
        SQL = SQL & "                      select w1.call_id, w1.ani, w1.max_start_time, w1.call_result, w1.tong_user,"
        SQL = SQL & "                            case (select min(call_result) from t_call_history u1"
        SQL = SQL & "                                  where(w1.ani = u1.ani)"
        SQL = SQL & "                                  and w1.call_result = 1"
        SQL = SQL & "                                  and w1.next_start_time = u1.TONG_START_TIME) when 3 then 'N' when 4 then 'N' when 5 then 'N' else 'Y' end next_call_result"
        SQL = SQL & "                        from (   "
        SQL = SQL & "                           select t1.call_id, t1.ani, t1.max_start_time, t2.call_result, t2.tong_user,"
        SQL = SQL & "                                   (select min(max_start_time) from "
        SQL = SQL & "                                          (select call_id, ani, max(tong_start_time) max_start_time from t_call_history"
        SQL = SQL & "                                          group by call_id, ani) sub1"
        SQL = SQL & "                                       where sub1.max_start_time > t1.max_start_time"
        SQL = SQL & "                                         and sub1.ani = t1.ani) next_start_time  "
        SQL = SQL & "                              from"
        SQL = SQL & "                                (select call_id, ani, max(tong_start_time) max_start_time, max(call_result) call_result from t_call_history"
        SQL = SQL & "                                where COM_CD = '" & gsCOM_CD & "'"

        If Not isInit Then
            SQL = SQL & " AND substr(tong_start_time,1,8) >= '" & dpt1.Text.ToString.Replace("-", "") & "'"
            SQL = SQL & " AND substr(tong_start_time,1,8) <= '" & dpt2.Text.ToString.Replace("-", "") & "'"
            SQL = SQL & " AND substr(tong_start_time,9,8) >= '" & cbH1.Text & cbT1.Text & "00" & "'"
            SQL = SQL & " AND substr(tong_start_time,9,8) <= '" & cbH2.Text & cbT2.Text & "00" & "'"
            SQL = SQL & " AND ani LIKE  '" & txtTongNo.Text.Trim & "%'"
            SQL = SQL & " AND TONG_USER LIKE  '" & cboUser.SelectedValue.ToString.Replace("XXXX", "") & "%'"
        Else
            Dim tm As String = Format(Now, "yyyyMMdd")
            SQL = SQL & " AND substr(tong_start_time,1,8) >= '" & tm & "'"
        End If


        SQL = SQL & "                                group by call_id, ani) t1,"
        SQL = SQL & "                                t_call_history t2"
        SQL = SQL & "                            where t2.COM_CD = '" & gsCOM_CD & "'"

        If Not isInit Then
            SQL = SQL & " AND substr(t2.tong_start_time,1,8) >= '" & dpt1.Text.ToString.Replace("-", "") & "'"
            SQL = SQL & " AND substr(t2.tong_start_time,1,8) <= '" & dpt2.Text.ToString.Replace("-", "") & "'"
            SQL = SQL & " AND substr(t2.tong_start_time,9,8) >= '" & cbH1.Text & cbT1.Text & "00" & "'"
            SQL = SQL & " AND substr(t2.tong_start_time,9,8) <= '" & cbH2.Text & cbT2.Text & "00" & "'"
            SQL = SQL & " AND t2.ani LIKE  '" & txtTongNo.Text.Trim & "%'"
            SQL = SQL & " AND t2.TONG_USER LIKE  '" & cboUser.SelectedValue.ToString.Replace("XXXX", "") & "%'"
        Else
            Dim tm As String = Format(Now, "yyyyMMdd")
            SQL = SQL & " AND substr(t2.tong_start_time,1,8) >= '" & tm & "'"
        End If

        SQL = SQL & "                              and t1.call_id = t2.call_id"
        SQL = SQL & "                              and t1.ani = t2.ani"
        SQL = SQL & "                              and t1.max_start_time = t2.tong_start_time"
        SQL = SQL & "                              and t1.call_result = t2.call_result"
        SQL = SQL & "                              order by max_start_time"
        SQL = SQL & "                              ) w1"
        SQL = SQL & "                            ) y1 "
        SQL = SQL & "                             order by max_start_time"
        SQL = SQL & "                ) r1"
        SQL = SQL & "                where r1.needKeep = 'Y'              "
        SQL = SQL & "                 /* order by max_start_time */"
        SQL = SQL & "               union all"
        SQL = SQL & "                select tong_telno call_id, tong_telno ani,CONCAT(tond_dd, tong_time) max_start_time,"
        SQL = SQL & "                                          '2' call_result, tong_user"
        SQL = SQL & "                  from t_customer_history"
        SQL = SQL & "                where call_type = 2"
        SQL = SQL & "                  and COM_CD ='" & gsCOM_CD & "'"

        If Not isInit Then
            SQL = SQL & " AND tond_dd >= '" & dpt1.Text.ToString.Replace("-", "") & "'"
            SQL = SQL & " AND tond_dd <= '" & dpt2.Text.ToString.Replace("-", "") & "'"
            SQL = SQL & " AND tong_time >= '" & cbH1.Text & cbT1.Text & "00" & "'"
            SQL = SQL & " AND tong_time <= '" & cbH2.Text & cbT2.Text & "00" & "'"
            SQL = SQL & " AND tong_telno LIKE  '" & txtTongNo.Text.Trim & "%'"
            SQL = SQL & " AND tong_user LIKE  '" & cboUser.SelectedValue.ToString.Replace("XXXX", "") & "%'"
        Else
            Dim tm As String = Format(Now, "yyyyMMdd")
            SQL = SQL & " AND tond_dd >= '" & tm & "'"
        End If

        SQL = SQL & "                ) s1"
        SQL = SQL & "            ) b left join t_customer_history a"
        SQL = SQL & "     on a.tong_telno = b.ani"
        SQL = SQL & "    and CONCAT(a.tond_dd, a.tong_time) = b.max_start_time "

        If Not isInit Then
            If cboCallResult.SelectedItem.ToString.Trim() = "전체" Then
                SQL = SQL & " "
            ElseIf cboCallResult.SelectedItem.ToString.Trim() = "부재중" Then
                SQL = SQL & " WHERE b.call_result in ('1', '4') "
            ElseIf cboCallResult.SelectedItem.ToString.Trim() = "수신" Then
                SQL = SQL & " WHERE b.call_result in ('3','5') "
            ElseIf cboCallResult.SelectedItem.ToString.Trim() = "발신" Then
                SQL = SQL & " WHERE b.call_result = '2'"
            End If
        End If

        SQL = SQL & "order by max_start_time"
        Return SQL
    End Function

    Public Sub gsSelect(ByVal isInit As Boolean)
        Try

            Dim SQL As String = genQuery(isInit)

            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            '************************************ 체크하자
            Dim dt1 As DataTable = DoQueryNoErrorCatch(gsConString, SQL)
            DataGridView2.DataSource = Nothing


            DataGridView2.Columns.Clear()

            DataGridView2.DataSource = dt1
            DataGridView2.Columns.Item(0).HeaderText = "통화번호"
            DataGridView2.Columns.Item(0).Width = 100
            DataGridView2.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            '.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            '.DefaultCellStyle.Format = "f2"
            '.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight

            DataGridView2.Columns.Item(1).HeaderText = "고객명"
            DataGridView2.Columns.Item(1).Width = 150
            DataGridView2.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            DataGridView2.Columns.Item(2).HeaderText = "통화일자"
            DataGridView2.Columns.Item(2).Width = 80

            DataGridView2.Columns.Item(3).HeaderText = "통화시간"
            DataGridView2.Columns.Item(3).Width = 80

            DataGridView2.Columns.Item(4).HeaderText = "통화자"
            DataGridView2.Columns.Item(4).Width = 120

            DataGridView2.Columns.Item(5).HeaderText = "통화결과"
            DataGridView2.Columns.Item(5).Width = 120

            DataGridView2.Columns.Item(6).HeaderText = "콜유형"
            DataGridView2.Columns.Item(6).Width = 100

            DataGridView2.Columns.Item(7).HeaderText = "상담결과"
            DataGridView2.Columns.Item(7).Width = 120

            dt1 = Nothing
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Catch ex As Exception
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Call WriteLog("FRM_CUSTOMER : " & ex.ToString)
        Finally
            result = 1
        End Try
    End Sub

    Public Sub gsFormExit()
        Try
            Me.Close()
        Catch ex As Exception
            Call WriteLog("FRM_CALLER_LIST : " & ex.ToString)
        End Try
    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        '조회
        Call gsSelect(False)
    End Sub


    Private Sub FRM_CALLER_LIST_Deactivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Deactivate
        Call gsFormExit()
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try
            With SaveFileDialog1
                .CheckPathExists = True
                .Filter = "Excel통합문서(*.xlsx)|*.xlsx|Excel97-2003문서(*.xls)|*.xls"
                .FileName = "고객상담이력" & "_" & Format(Now, "yyyyMMdd")
                .Title = "고객상담이력 엑셀로 내보내기"
                .ShowDialog()
            End With
        Catch ex As Exception
            WriteLog(ex.ToString)
        End Try

    End Sub

    Private Sub SaveFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Try
            result = 0
            Call gsSelect(False)
            While (result = 0)
                Threading.Thread.Sleep(1000)
            End While
            Call Excel_Export2(SaveFileDialog1.FileName, SaveFileDialog1.Title, DataGridView2, "0,1,2,")
        Catch ex As Exception
            WriteLog(ex.ToString)
        End Try
    End Sub

    Private Sub DataGridView2_CellContentDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellContentDoubleClick
        Try
            If e.RowIndex < 0 Then Exit Try
            Dim i As Integer = e.RowIndex

            With DataGridView2.Rows(i)

                Dim telNo As String = .Cells(0).Value.ToString
                Dim tongDate As String = .Cells(2).Value.ToString
                Dim tongTime As String = .Cells(3).Value.ToString
                Dim frm As FRM_MAIN = Me.MdiParent
                Call frm.OpenCustomerPopupMod(telNo, tongDate, tongTime)
            End With
        Catch ex As Exception
            Call WriteLog(ex.ToString)
        End Try
    End Sub


    Private Sub Search_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dpt1.KeyDown, dpt2.KeyDown, dpt2.KeyDown, dpt2.KeyDown, cbH1.KeyDown, cbH2.KeyDown, cbT1.KeyDown, cbT2.KeyDown, txtTongNo.KeyDown, cboCallResult.KeyDown, cboUser.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call gsSelect(False)
        End If
    End Sub

    Private Sub DataGridView2_CellPainting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles DataGridView2.CellPainting
        If e.Value IsNot Nothing And e.RowIndex > 0 Then
            If Me.DataGridView2.Item(5, e.RowIndex).Value.ToString.Trim() = "부재중" Then '5 = "통화결과"
                e.CellStyle.ForeColor = Color.Red
            End If
        End If
    End Sub
End Class