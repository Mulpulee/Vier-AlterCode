Dialogue_0004 = CreateDialog(function()

    Talk("cat", "경비가 살벌한데?")
    Talk("player", "내가 탈출하면서 늘어난 게 아닐까.")
    Talk("cat", "그럴지도 모르지.")
    Talk("cat", "어때, 긴장돼?")
    
local select = MakeSelect("player",
    {
        "엄청나게.",
        "딱히."
    })

    if select == 0 then
        Talk("cat", "그게 일반적인 반응일거야.")
        Talk("cat", "너도 뿌리는 인간이잖아?")
        Talk("cat", "긴장 풀어.")
        Talk("cat", "복수를 하고 나면 모든게 나아질 거야.")
    elseif select == 1 then
        Talk("cat", "생각보다 강심장이구나?")
        Talk("cat", "뭐, 그정도 담력이 아니면 힘들긴 하지.")
    end

    Talk("player", "…있잖아.")
    Talk("cat", "냥?")
    Talk("player", "날 도와줄 이유가 있었어?")
    Talk("player", "그냥 거기에서 죽게 내버려 뒀어도")
    Talk("player", "너에겐 더 이득이었을텐데. 왜냐면 난…")

    Talk("cat", "쓸데없는 걱정 마시고.")
    Talk("cat", "옛날생각이 나서 그래.")
    Talk("cat", "아무튼, 사연팔이는 여기까지 하자고.")
    Talk("cat", "자, 가자.")

end)