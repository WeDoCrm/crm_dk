Public Module ConstDef
    Public Enum ActionStatus
        OpenEmpty = 0
        PopUpInBound = 1
        PopUpOutBound = 2
        PopUpUserExist = 3  '사용자정보, 통화정보 올라온 상태
        PopUpNoUser = 4     '사용자정보 없고 통화정보 올라온 상태
        PopUpDetail = 5     '상담이력 수정 상태
        OpenUserSearched = 6       '사용자정보 조회 - 아웃바운드준비상태
        OpenNoUserSearched = 7 '사용자정보 조회없음 
    End Enum

    Public Enum PANEL_FOCUS
        NONE = -1
        CUSTOMER_INFO = 0
        CONSULT_INFO = 1
        CONSULT_HISTORY = 2
    End Enum
End Module
