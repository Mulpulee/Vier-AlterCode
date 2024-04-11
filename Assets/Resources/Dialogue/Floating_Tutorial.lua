Floating_Tutorial = CreateDialog(function()

    Talk("Cat", "자. 기초부터 시작해보자.")
    Talk("Cat", "좌/우 화살표를 눌러 이동할 수 있어.")
    Talk("Cat", "스페이스바를 누르면 점프할 수 있지.")
    Talk("Cat", "Q는 막기, W는 대쉬, E는 가벼운 공격이야.\n이정도는 쉽지?")
    
    Talk("Cat", "지금부터가 중요해. 놓치지 않게 천천히 설명해줄게.")
    Talk("Cat", "숫자 키를 눌러 메모리를 교체할 수 있어.\n다시 한 번 말하지만, 손을 써 뒀으니 걱정하진 말고.")
    KeyInput("Cat", "1번 키를 눌러 <color=yellow>방어형 메모리</color>로 교체해 봐.", 49)
    Talk("Cat", "좋아, 각 메모리는 <color=green>특별한 스킬과 패시브</color>를 가지고 있지.\n바뀐 능력들은 화면 오른쪽 아래에서 확인할 수 있어.")
    KeyInput("Cat", "바뀐 메모리에선 E가 기본 공격이 아닌 <color=green>특수 스킬</color>로 변해.\n한 번 사용해 봐.", 101)
    Talk("Cat", "잘했어! 메모리는 1번부터 4번까지 <color=yellow>방어형</color>, <color=red>근거리 공격형</color>,"
            .. " <color=#00ffffff>원거리 공격형</color>, <color=purple>지원형</color>이 있어.\n상황에 따라 적절하게 변경하며 활용하도록 해.")
    Talk("Cat", "자, 설명은 여기서 끝. 훈련용 로봇을 하나 세워둘테니 자유롭게 연습해 봐.\n충분히 연습하고, 로봇으로 돌아오면 돼.")

end)