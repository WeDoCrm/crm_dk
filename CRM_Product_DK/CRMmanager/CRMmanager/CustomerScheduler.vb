Public Class CustomerScheduler

    Dim mPeriod As Int16      '알람주기
    Dim mStartTime As Int16  '알람시작시간

    '로그인 사람의 일정목록을 읽고,
    '수행예정인 업무를 정해진 알람주기와 시작시간에 보여준다.
    '

    Sub FindScheduleList()
        Try
            Dim temp As String
            Dim dt As DataTable


            temp = "SELECT S_START_TIME, S_COMPANY_COWORKER, S_TITLE, S_DESC, SWORKOUT_REASON, S_WORKOUT_LOC From t_schedule  "
            temp2 = " Where COM_CD='" & gsCOM_CD & _
                   "' AND S_START_TIME='" & dpt333.Text.ToString.Replace("-", "") & drpHour3.Text & drpMin3.Text & _
                   "' AND REGISTRANT LIKE '" & gsUSER_ID & ".%" & _
                   "' AND S_COMPANY_COWORKER LIKE '%" & cboCoWorker3.SelectedIndex.ToString & "%" & _
                   "' AND SHARING_TYPE='O'"
            '"' AND S_TITLE='" & txtTitle.Text.Trim & "' "

            dt = MiniCTI.DoQueryNoErrorCatch(gsConString, temp & temp2)

            If dt.Rows(0).Item(0).ToString.Trim = "0" Then

                temp = "Insert into t_schedule(COM_CD,S_START_TIME,S_END_TIME,REGISTRANT,SHARING_TYPE,S_TITLE,S_COMPANY_COWORKER,S_DESC,S_WORKOUT_REASON,S_WORKOUT_LOC) values('"
                temp = temp & gsCOM_CD & "','" & dpt333.Text.ToString.Replace("-", "") & drpHour3.Text & drpMin3.Text & "','"
                temp = temp & dpt333.Text.ToString.Replace("-", "") & "2359" & "','"
                temp = temp & gsUSER_ID.Trim & "." & gsUSER_NM.Trim & "','O','"
                temp = temp & "외근등록" & "','" & cboCoWorker3.SelectedValue.ToString & "','" & txtWorkContents3.Text.Trim & "'"    '& "') "

                If cboWorkReason3.SelectedValue.ToString.Replace("XXXX", "") <> "" Then
                    Dim str() As String = cboWorkReason3.SelectedValue.ToString.Split(".")
                    temp = temp & " ,'" & str(0).Trim & "'"
                Else
                    temp = temp & " ,''"
                End If
                'End If

                temp = temp & ",'" & txtWorkArea3.Text.Trim & "') "
                'temp = temp & ",'" & "" & "') "
                temp2 = ""

            Else
                temp = " UPDATE t_schedule SET S_WORKOUT_LOC = '" & txtWorkArea3.Text.Trim & "'" & " , S_DESC ='" & txtWorkContents3.Text.Trim & "'"
                'temp = " UPDATE t_schedule SET S_WORKOUT_LOC = '" & txtWorkArea3.Text.Trim & "'" & " , S_DESC ='" & txtWorkContents3.Text.Trim & "'"
                If cboWorkReason3.SelectedValue.ToString.Replace("XXXX", "") <> "" Then
                    Dim str() As String = cboWorkReason3.SelectedValue.ToString.Split(".")
                    temp = temp & " , S_WORKOUT_REASON =  '" & str(0).Trim & "'"
                Else
                    temp = temp & " , S_WORKOUT_REASON =  '" & "" & "'"
                End If
            End If
            dt.Reset()

            dt = MiniCTI.DoQueryParam(gsConString, temp & temp2)
            MsgBox("처리되었습니다.", MsgBoxStyle.OkOnly, "정보")
            dt = Nothing

        Catch ex As Exception
            Call WriteLog("외부업무 등록 실패:" & ex.Message)

            MsgBox("등록에 실패했습니다.", MsgBoxStyle.OkOnly, "정보")
        End Try
    End Sub
End Class
