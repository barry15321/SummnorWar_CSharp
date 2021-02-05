魔靈召喚按鍵精靈腳本 (.Net版本)
================


腳本開啟 BlueStacks + 魔靈召喚後開啟腳本程式。
開啟 ImageLoad 介面選擇並儲存截圖影像
適用於自動刷火山、次元裂縫、英雄地下城腳本


*   [開發環境](#environment)

*   [使用介紹](#introduce)

*   [Key Action](#action)

* * *


<h1 id="environment">開發環境</h1>

Window7/10 + Visual Studio (.net 4.5.2) + Bluestacks。

<h1 id="introduce">使用介紹</h1>

腳本介面上 Menu 選項中：
第一項為查看當前所有視窗（debug用
第二項為執行腳本程式，在 Bluestacks 開啟以及遊戲開啟後才執行。
第三項為影像儲存視窗，開啟視窗後可新增/刪除/清空目前截圖儲存列表
列表中，腳本將依照截圖順序發送 A B C D E ... 等按鍵
（在 Bluestack 已經設置好按鍵點擊位置）

主程式標題視窗上發送提示訊息。

詳情參照 [Medium]
  [Medium]: https://medium.com/@replacedtoy/%E7%94%A8-gdi-%E5%B0%B1%E5%AF%AB%E5%87%BA%E8%87%AA%E5%8B%95%E5%8C%96%E9%81%8A%E6%88%B2%E8%85%B3%E6%9C%AC-ft-bluestack-10fc8a6da4d2

ps：模擬器選項繪圖方式使用 DirectX，勿用 OpenGL。

<h1 id="action">Key Action : </h1>

F1 : 按鍵判圖點擊開啟。

F2 : 按鍵判圖點擊關閉。

F3 : 擷取當前畫面並與第 Index 張影像比對 

F4 : 擷取當前畫面並存檔

F5 : Index += 1 

F6 : Index = 0

F7 : 模擬當前 Index 所對應點擊 Bluestack 的事件
