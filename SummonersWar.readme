Summoner's War 按鍵精靈 (VB.Net)
==================

**NOTE:** Demonstrate how this program works and what we should do in advance.
================

*   [開發環境](#environment)

*   [程式介紹](#introduce)

*   [Step](#step)

*   [Key Action](#action)

* * *


<h1 id="environment">開發環境</h1>

Window 7 + Visual Studio (.net 4.5.2) + Bluestack。
程式多引用 user32.dll，理論上即使在不同 .net 版本上開發、使用，應該也不會有太多差距。


<h1 id="introduce">程式介紹</h1>
搭配 Bluestack 模擬器內鍵盤操作設定功能，對 Bluestack 截圖判斷當前狀況，並依照條件點擊指定位置，完成背景按鍵精靈輔助程式

程式開啟後將搜尋 Bluestack，如果有成功抓到 Bluestack，便會將 Bluestack 拉進 winform 表單內。
標頭則會顯示 Bluestack 的 hwnd，反之則是 0。
而目前 vb 版本載入圖片路徑仍為絕對路徑，還未能到達客製化使用。

至於程式直接擷取 Bluestack 畫面，就我所知有兩種方法。一種為 GDI 繪圖、另一種則是 DirectX 繪圖。
但 DirextX 繪圖擷取影像難度以及呼叫的 api 較多，所以採用了 GDI 繪圖擷取影像的方法，若有興趣了解更詳細的內容可以參考 [關於截圖這回事。][capturesrc]。

  [capturesrcsrc]: https://medium.com/p/a4b0aff8178f/edit

擷取到正確圖片後，使程式發送模擬按鍵訊息。

<h1 id="step">Step</h1>
開啟模擬器、魔靈

開啟按鍵精靈

通常用來刷火山副本、試煉之塔、地下城（舊版地下城刷完碎片是隨機掉落 1~2片）

<h1 id="action">Key Action : </h1>
F1 : 按鍵判圖點擊開啟。
F2 : 按鍵判圖點擊關閉。
F3 : 擷取當前畫面並與第 Index 張影像比對 
F4 : 擷取當前畫面並存檔
F5 : Index += 1 
F6 : Index = 0
F7 : 模擬當前 Index 所對應點擊 Bluestack 的事件