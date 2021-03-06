モデリング会 β版: 会議室予約ドメイン
==========================

<!---
## 開催時の検討事項
- アイスブレイクを明示的にいれるかどうか決める
- ディスプレイ
- 付箋とか

--->

## 動機
モデリングおもしれー！

↓

モデリング会したい！ 

↓

お題がねえ！

↓

お題をつくってみた == 会議室予約ドメイン(後述)

↓

でも、お題として洗練されてねえ！

↓

実際にやってみて、お題を洗練させよう！ & 普通にモデリングもしちゃおう！


## 日時と場所
- 日時: 2019年12月21日(土) 13:00 - 16:00
- 場所: [神楽坂プラザビル](https://www.sanko-e.co.jp/search/details/1036704)
  - ＪＲ線 飯田橋駅 徒歩8分くらい
  - 有楽町線 飯田橋駅 徒歩4分くらい
  - 都営大江戸線 牛込神楽坂駅 徒歩5分くらい

## タイムテーブル(トータル: 3.0h)
- 15m: 趣旨説明とかやり方の説明
- 30m: セッション1: 探求してみる
- 30m: セッション2: 実装してみる(あくまでモデリングのための実装)
- 30m: セッション3: 洗練してみる
- 15m: みんなで振り返り
- 15m: **気づいたことの言語化(3ツイートする)**
- 30m: バッファ
- 参考: [割り勘ドメインモデリングワークショップ](https://github.com/j5ik2o/warikan-domain)



# 以下、モデリングお題 「会議室予約ドメイン」
## システム化に至る背景
### 現状: いまこんなんなっている
現在の会議室予約ルールが全くなくて、Googleカレンダーで管理をしている状態。
そのため、会議室をみだりに予約する社員が多発し、本当に必要な社員が会議室を予約できない状況が続いている。

### 困り事:
- みだりに予約する人が多く、直前でのキャンセルや、会議はとりやめになったのに会議室予約をキャンセルしないことがよくある
    - 1ヶ月前なら、誰でも、何度でも、予約できるし、ペナルティも存在しない
- 「会議室の予約がとれない」という声が多いが、実際の会議室の利用実績がわからない
    - 人数的には、設備は十分に足りているはずなのに。。。
- システム上には現れない、会議室の交換があったりする

### 理想: こうなれば嬉しい
- 必要な会議のために、きちんと会議室が利用されている状態
- 会議室がどれだけ利用されているかが正しく把握できている状態
    - 不足しているなら増設できるし、過剰なのであれば倉庫として活用するなどしたい

### 対策と予約ルール
上記の困り事を解決するために、会議室を予約する際にいくつかのルールを定める。

- ルール1: 予約は15分単位であること。
- ルール2: 予約できる時間帯は 10:00から19:00 までとする。
- ルール3: 予約の最大時間の制限はない。実質的には最大9時間。
- ルール4: 予約は30分の延長が1回だけできる。ただし、後ろの予約がある場合は負荷とする。
- ルール5: 会議室を予約できるのは、使用したい日の30日前(休日も込み)からとする。時間帯は関係なし。
- ルール6: 同一の人物が、ダブルブッキングは不可とする。
    - ex: Aさんが、 10時から12時の会議 がある状態で、同日の11時から13時の会議予約はできない

ルールを人力で管理するのは難しいため、ソフトウェアを使って解決したい。

### 会議室についての情報
- キャパシティ8人の会議室が3部屋ある


## アクター
- 予約者: 会議を行うための会議室を予約する役割
    - 要望
        - 会議室の空き状況をすぐ確認したい
        - 会議室をスムーズに予約できるようにしたい
- 会議参加者: 会議に参加する人
- 施設管理者: 会議室を利用状況などなどを管理する人
    - 要望
        - 予約された会議室がちゃんと使用されているかどうかを把握したい
        - ある期間において、会議室がどれだけ使用されているのかどうかを把握したい
        - ある期間において、誰がどれだけ会議室を利用しているのかを把握したい
- 経理
    - 備品を補充する話とかになると必要かもしれない
    - でも、やっぱりスコープ外な気がする



## ユースケース
### スコープ外 の話
- 誰かが会議を設定する

### 主語: 予約者
- 予約者 が 会議室 を 予約する
- 予約者 が 会議室 を キャンセルする
- 予約者 が 会議室 の 予約時間 を変更する

### 主語: 施設管理者
- 施設管理者 が 会議室 の 利用状況 を 把握する
    - 利用状況 == 「実際に今、予約したとおりに、会議室が使用されているかどうか」
- 施設管理者 が 誰が どれだけ 会議室 を 利用しているか を 把握する
    - 「誰」に含まれる人は何か？ → 予約者？ 会議参加者？
- 施設管理者 が 会議室 の 利用実績 を 把握する
    - 実績 == ある期間において、どの会議室が どれだけ利用されたかどうか
        - ex1: 全く使われない会議室があるなら、減らすという意思決定がありえる
        - ex2: 非常に使われている会議室があるなら、会議室を増やすという意思決定がありえる


## 雑記
### 思いついたルール候補のたまり場(暫定版)
- 会議室はいくつかの種類があると思うけど、どんな種類があるのかな？
- 会議室がもつ属性ってなんだろう？
    - 収容人数
    - 机、イス、ホワイトボード、プロジェクターなどなど
    - 属性
        - 利用する人に関する制限？ 執務室(社内のみ)、社外用(応接室とか)、どっちでもOK
        - 複数の会議室をぶち抜けるとか、そういうの
    - っていうか、備品は借りるとかにするとかもありえるね
- 備品のルールどうしよう？
    - ex1: すべての会議室で同じ備品がある(難易度: 低)
    - ex2: 会議室によって備品が異なる(難易度: 低)
    - ex3: 会議室には備品はなく、共用の備品を借りる形式にする(難易度: 高) <--- スコープ外かも
- 予約時のルールはどうしよう？
    - 延長はできるのか？
    - いつから予約が可能になるのか？
    - 前後の予約はどれだけの時間間隔でいいのか？(ex: 必ず5分はあける、連続でOK)
    - 1回で使える時間は最大でどれだけ？
    - 1回で使える時間は最小でどれだけ？(ex: 1分の会議はありえるのか？)
    - 予約回数の制限はあるのか？ (ex: 月にN回まで。N回を超える場合は申請が必要とか)
    - 会議室がぶち抜ける場合どうする？
    - 予約するための、必須情報はなんだろう？(ex: 人数が不確定でも予約はできるのか？)
        - 人数、用途(？)、使用する備品、日時、参加者の詳細(これは不要かも？)、社内会議なのか社外会議なのか
- ペナルティはあるのか？ どうするのか？ <--- 考えなくてもいいかもしれない
    - 直前でのキャンセル(ってか、直前って何？)
    - 予約したけど、来ない



---

# 実装編

## 目的
- オンラインでのモブユースケース実行&モデリング


ユースケース図
![image](https://user-images.githubusercontent.com/33717710/71358998-42a7b800-25ce-11ea-8ebe-43b3835142a8.png)

モデル図
![image](https://user-images.githubusercontent.com/33717710/71359321-67e8f600-25cf-11ea-87aa-65666f82bf7b.png)



---

# プロジェクト間の依存構成

![image](https://user-images.githubusercontent.com/33717710/71788536-a918e580-3066-11ea-8754-576b81383071.png)


以上

