Imports System.Xml

Public Class FRM_CONFIG

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        ''1.write  xml
        Dim doc As XmlDocument = New XmlDocument()
        'Dim m_nodelist As XmlNodeList
        'Dim m_node As XmlNode

        doc.Load(file_path & config_file)
        Dim environmentNode As XmlNode = doc.SelectSingleNode("/environment")

        Dim customerEl As XmlElement

        customerEl = doc.SelectSingleNode("/environment/customer")
        If customerEl Is Nothing Then
            customerEl = doc.CreateElement("customer")
            environmentNode.AppendChild(customerEl)
        End If

        'useTongUser
        Dim useTongUser As XmlElement
        useTongUser = customerEl.SelectSingleNode("useTongUser")
        If useTongUser Is Nothing Then
            useTongUser = doc.CreateElement("useTongUser")
        End If
        useTongUser.InnerText = If(ckbUseTongUser.Checked, "Y", "N")
        customerEl.AppendChild(useTongUser)

        'useUserDef
        Dim useUserDef As XmlElement
        useUserDef = customerEl.SelectSingleNode("useUserDef")
        If useUserDef Is Nothing Then
            useUserDef = doc.CreateElement("useUserDef")
        End If
        useUserDef.InnerText = If(ckbUseUserDef.Checked, "Y", "N")
        customerEl.AppendChild(useUserDef)

        'noCloseOnSave
        Dim noCloseOnSave As XmlElement
        noCloseOnSave = customerEl.SelectSingleNode("noCloseOnSave")
        If noCloseOnSave Is Nothing Then
            noCloseOnSave = doc.CreateElement("noCloseOnSave")
        End If
        noCloseOnSave.InnerText = If(ckbNoCloseOnSave.Checked, "Y", "N")
        customerEl.AppendChild(noCloseOnSave)

        'useAlarm
        Dim useAlarm As XmlElement
        useAlarm = customerEl.SelectSingleNode("useAlarm")
        If useAlarm Is Nothing Then
            useAlarm = doc.CreateElement("useAlarm")
        End If
        useAlarm.SetAttribute("enabled", If(ckbUseAlarm.Checked, "Y", "N"))

        'alarmTime
        Dim alarmTime As XmlElement
        alarmTime = useAlarm.SelectSingleNode("alarmTime")
        If alarmTime Is Nothing Then
            alarmTime = doc.CreateElement("alarmTime")
        End If
        alarmTime.InnerText = If(ckbUseAlarm.Checked, nupAlarmStart.Value.ToString, "0")
        useAlarm.AppendChild(alarmTime)

        'alarmPeriod
        Dim alarmPeriod As XmlElement
        alarmPeriod = useAlarm.SelectSingleNode("alarmPeriod")
        If alarmPeriod Is Nothing Then
            alarmPeriod = doc.CreateElement("alarmPeriod")
        End If
        alarmPeriod.InnerText = If(ckbUseAlarm.Checked, nupAlarmPeriod.Value.ToString, "0")
        useAlarm.AppendChild(alarmPeriod)

        doc.Save(file_path & config_file)

        '2. refresh global 
        gbUseTongUser = ckbUseTongUser.Checked
        gbUseUserDef = ckbUseUserDef.Checked
        gbNoCloseOnSave = ckbNoCloseOnSave.Checked

        Me.Close()

    End Sub

    Private Sub FRM_CONFIG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ckbUseTongUser.Checked = gbUseTongUser
        ckbUseUserDef.Checked = gbUseUserDef
        ckbNoCloseOnSave.Checked = gbNoCloseOnSave
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class