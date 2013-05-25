xamarinGMS
==========

Xamarin Google Maps SDK Sample in Geomedia Summit OSAKA 2013

このサンプルはジオメディアサミット2013大阪懇親会会場でのライトニングトークをベースにしたサンプルです。

ジオメディアサミット2013大阪については http://atnd.org/event/gmsosaka2013 、ライトニングトークのスライドについては http://www.slideshare.net/kokogiko/gms2013-xamarin-googlemapssdk を参照してください。

Xamarinの入手
---------------

詳細については割愛しますが、 http://xamarin.com/ からXamarin Studioを入手してください。  
その後、本githubのxamarinGMSリポジトリをチェックアウトし、Xamarin StudioでGeomediaSummit.slnを起動してください。  
利用しているライブラリの規模等の問題により、Starterエディションモードではコンパイルできません。1ヶ月のTrialモード(或いは有償のモード)で実行ください。

Google Play Serviceコンポーネントの導入 (Android)
---------------
amay077 さんの http://qiita.com/items/2d76a090d49926805431iOS こちらの記事に従って、Google Play servicesをXamarin上でセットアップします。  
その後、GeomediaSummitソリューション中のGooglePlayServicesプロジェクトはリンク切れになっていると思いますが、これを削除し、代わりに
amay077さん記事中のMapsAndLocationDemoソリューションに含まれている、GooglePlayServicesプロジェクトを追加してやります。  
また、GeomediaSummit.ViewDroidプロジェクトの参照フォルダを右クリックし、「参照アセンブリの編集」=>「Projectsタブ」で、追加したGooglePlayServicesへの参照を追加してやります。

GoogleMapsコンポーネントの追加 (iOS)
---------------

