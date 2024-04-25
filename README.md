# ⚔ SpartaDungeon
던전을 떠나기 전 마을에서 장비를 구하는 텍스트형 RPG 게임 구현 (C# - Console App)

## 🎮 프로젝트 소개
스파르타 코딩클럽 내일배움캠프의 첫번째 **개인** 프로젝트형 과제.

### ⌛ 제작 기간
**24-04-22 ~ 24-04-25**

### 🙋‍♂️ 제작 인원
**김민우**

## 💡 주요 기능

### 게임 시작 화면
- 게임 시작시 간단한 소개 말과 마을에서 할 수 있는 행동을 알려줍니다.
- 원하는 행동의 숫자를 타이핑하면 실행합니다.

### 1. 상태 보기
- 캐릭터의 정보를 표시합니다.
- 레벨 / 이름 / 직업 / 공격력 / 방어력 / 체력 / Gold , 총 7개의 속성을 가지고 있습니다.
- 이후 장착한 아이템에 따라 수치가 변경 될 수 있습니다.

### 2. 인벤토리
- 보유 중인 아이템을 전부 보여줍니다.
이때 장착중인 아이템 앞에는 [E] 표시를 붙여 줍니다.
- 처음 시작시에는 아이템이 없습니다.

### 3. 상점
- 보유중인 골드와 아이템의 정보,가격이 표시됩니다.
- 아이템 정보 오른쪽에는 가격이 표시가 됩니다.
- 이미 구매를 완료한 아이템이라면 **구매완료** 로 표시됩니다.
  
### 4. 던전
- 던전은 3가지 난이도가 있습니다.
- 방어력으로 던전을 수행할 수 있을지 판단합니다.
- 공격력으로 던전 클리어시 보상을 어느정도 얻을지 계산됩니다.
### 5. 휴식
- 휴식을 선택하면 500G 으로 체력을 회복합니다.
- 보유 금액이 충분하다면
  → **휴식을 완료했습니다.** 출력
- 보유 금액이 부족하다면
  → **Gold 가 부족합니다.** 출력
- 휴식시 체력은 100까지 회복됩니다.
- 보유 골드 표시
