# 概要
MicrosoftのGraph APIを用いた、イベントキャンセルのためのソースコードです。Azure Functions(v1)で用いることを想定しています。<br>
This is a sample for cancelling event using Microsoft Graph API. You can use this code on Azure Functions(v1). 

# keyに関して
必要なキーはAzureポータル、およびGraphエクスプローラーを用いて取得してください。<br>
You can get some keys from both Azure portal and Graph Explorer.

# Graph APIに関して
以下のGraph APIを使用しました。<br>
The API I used was <br>
https://docs.microsoft.com/ja-jp/graph/api/event-cancel?view=graph-rest-beta

# 参考
以下のブログを参考にさせて頂きました。<br>
I referred the manuscripts (I apologize these are written in Japanese) <br><br>
https://sprestaurant.hatenablog.com/entry/2017/09/19/012247 <br><br>
https://blogs.technet.microsoft.com/junichia/2017/01/27/azure-functions-%E3%81%8B%E3%82%89%E5%AE%9A%E6%9C%9F%E7%9A%84%E3%81%AB-microsoft-graph-%E3%81%AB%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B9%E3%81%99%E3%82%8B/
