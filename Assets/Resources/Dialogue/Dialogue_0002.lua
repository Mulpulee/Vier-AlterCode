Dialogue_0002 = CreateDialog(function()

    Talk("player", "윽...")
    Talk("cat", "아직 움직이면 안돼.")

    Action("FadeIn");

local select = MakeSelect("player",
    {
        "누구?",
        "여긴 어디?"
    })

    if select == 0 then
        Talk("player", "누구?")
    elseif select == 1 then
        Talk("player", "여긴 어디?")
    end

    Talk("cat", "여긴 내 자가용 안이야.")
    Talk("cat", "작은 몸으론 위험해서 열심히 만들었지.")
    Talk("cat", "이걸로 널 여기까지 데려왔어.")
    Talk("cat", "난 개체번호 C-AT-1010.")
    Talk("cat", "너랑 같은 실험체 출신이지.")
    Talk("cat", "그렇게 경계하지 말라구.")
    Talk("cat", "원래 동류는 동류를 알아보는 법이니까.")
--    Talk("B", "저기 있는 스프라도 먹어.")
--    Talk("B", "손봐야 할 게 있으니까 기다리고 있으라구.")

--    Talk("B", "한 방울도 안 남기고 다 먹었구나.")
--    Talk("B", "잘 했어. <size=20>맛 없어서 빨리 처리해버리려고 했는데 다행이야.")
    Talk("cat", "CODE:ALTER.")
    Talk("cat", "보아하니 새 프로젝트의 실험체로구만?")
    Talk("cat", "용케도 탈출했군... 경비병력이 많았을텐데.")
    Talk("player", "...기억 안 나.")
    Talk("cat", "냥?")
    Talk("player", "기억이 안 나.")
    Talk("cat", "…그럴 수 있지. 괜찮아.")
    Talk("cat", "적어도 네 잘못은 아니니까.")
    Talk("player", "......고 싶어.")
    Talk("cat", "응?")
    Talk("player", "...부숴버리고 싶어.")
    Talk("player", "날 이렇게 만든 사람들을 뭉게버리고 싶어.")
    Talk("player", "...가슴 안에서 뭔가가 끓는 느낌이 들어. 튀어 나올 것 같이...")
    Talk("cat", "... ...")
    Talk("cat", "도와줄까?")
    Talk("player", "네가 어떻게?")
    Talk("cat", "다 방법이 있지.")
    
    Talk("player", "...")
    Talk("player", "...!")
    Talk("cat", "널 발견한 곳에 있던 거야.")
    Talk("player", "...메모리.")
    Talk("cat", "메모리?")
    Talk("player", "이걸 목에 꽂게 되면, 내가 아니게 되어버려.")
    Talk("player", "마치 다른 사람이 된 것처럼...`d1` 그 때의 기억이 없어.")
    Talk("cat", "...흐음.")
    Talk("player", "...`d1`이걸 사용하자고 할 건 아니지?")
    Talk("cat", "맞는데.")
    Talk("cat", "워, 워. 그런 반응 말라고.")
    Talk("cat", "다 방법이 있으니까.")
    Talk("cat", "난 너랑 같은 실험체 출신이야.")
    Talk("cat", "너의 복수에 가담하는 이유가 뭐겠어?")
    Talk("cat", "믿어줄거지?")
    Talk("player", "...")
    Talk("player", "그래.")
    Talk("cat", "좋아, `d1`최고의 복수를 해내자고.")

end)