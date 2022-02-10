Summoners War 自動化腳本程式 (.Net版本)
================


開啟 BlueStacks + 魔靈召喚後開啟腳本程式。
適用於自動刷火山、次元裂縫、英雄地下城腳本


*   [開發環境](#environment)

*   [使用介紹](#introduce)

*   [Key Action](#action)

* * *


<h1 id="environment">開發環境</h1>

Window7/10 + Visual Studio (.net 4.5.2) + Bluestacks。
建議使用舊版BS。（目前更新及開發使用3-54-65版本）

<h1 id="introduce">使用介紹</h1>

腳本介面上 Menu 選項中：
Menu ->
Setting\Window List 查看當前所有視窗（debug用）
Setting\Bluestack load 載入 Bluestacks 下視窗至監控視窗。
Setting\Image dir  開啟 ImageLoad 介面選擇並儲存擷圖影像 *- 仍在開發中

Moves\Capture Screen 對監控視窗擷取畫面
Moves\Script Start 腳本執行
Moves\Script Stop 腳本結束
Moves\Index 0 Set Index = 0
Moves\Index++ Set Index++;
Moves\Search Img[Index] 擷取監控畫面，並搜尋 Img[Index] 是否存在擷圖中
Moves\Click[Index]  對Bluestack發送按鍵事件

並請在 Bluestack 上設置好按鍵點擊位置(A,B,C,D,E,...)
自動化腳本會在搜尋到擷圖後發送相對應的 Index 按鍵
(Index=0 => Send A button , Index=1 => Send B button,and so on...)

ps：模擬器選項繪圖方式使用 DirectX，勿用 OpenGL。


[Medium的微介紹文](https://medium.com/@replacedtoy/%E7%94%A8-gdi-%E5%B0%B1%E5%AF%AB%E5%87%BA%E8%87%AA%E5%8B%95%E5%8C%96%E9%81%8A%E6%88%B2%E8%85%B3%E6%9C%AC-ft-bluestack-10fc8a6da4d2)

Demo

[Demostration_v1](https://youtu.be/xBsv0V9iw4I)

[Demostration_full](https://youtu.be/0xe06qxvdNY)


<h1 id="action">Key Action : </h1>

F1 : 腳本開始運行。

F2 : 腳本結束運行。

F3 : 擷取當前畫面並與第 Index 張影像比對 

F4 : 擷取當前畫面

F5 : Index = 0 

F6 : Index += 1

F7 : 模擬當前 Index 所對應點擊 Bluestack 的事件
