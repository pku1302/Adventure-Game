# Adventure Game
<img width="634" height="609" alt="image" src="https://github.com/user-attachments/assets/b5b6fbfa-69ff-44ce-8618-6dc0b9c6803c" />

> Unity 6 기반의 2D 액션 RPG 개인 프로젝트

![Main Gameplay](images/main.gif)

## 프로젝트 소개

플레이어는 던전을 탐험하며 몬스터를 처치하고 아이템을 획득할 수 있습니다.

획득한 장비를 강화하여 캐릭터를 성장시키고 더 강력한 던전에 도전할 수 있습니다.

### 개발 정보

| 항목    | 내용                |
| ----- | ----------------- |
| 개발 기간 | 2026.02 ~ 2026.06 |
| 개발 인원 | 1인 개발             |
| 엔진    | Unity 6           |
| 언어    | C#                |

### 주요 기능

* FSM 기반 몬스터 AI
* 상태이상 시스템
* MVP 패턴 기반 인벤토리
* 장비 및 강화 시스템
* 이벤트 기반 UI 갱신

---

# Gameplay

## 전투 시스템

![Combat](images/combat.gif)

<img width="1920" height="1080" alt="전투" src="https://github.com/user-attachments/assets/57f19824-e202-495f-b38c-c9b97763a949" />
플레이어는 원거리 공격을 통해 몬스터를 처치할 수 있으며,
몬스터는 FSM 기반 AI를 사용하여 행동합니다.



---

## 인벤토리 시스템

![Inventory](images/inventory.gif)
<img width="1920" height="1080" alt="루팅" src="https://github.com/user-attachments/assets/22a658b7-1ba9-4d3c-ab82-9f6d85a2e84a" />

* 아이템 획득
* 장비 장착
* 아이템 이동
* 드래그 앤 드롭

---

## 강화 시스템

![Enhance](images/enhance.gif)
<img width="1920" height="1080" alt="강화" src="https://github.com/user-attachments/assets/88995ade-3238-45d4-9211-3e6b9d5a28a1" />

장비를 강화하여 능력치를 향상시킬 수 있습니다.

---

## 상태이상 시스템

* Poison
* Heal
* Snare

상태이상마다 독립적인 라이프사이클을 가지도록 설계하였습니다.

---

# Technical Details

## FSM 기반 몬스터 AI

<img width="1177" height="637" alt="image" src="https://github.com/user-attachments/assets/bdc3f53d-591a-4917-893d-c724375b20db" />

### 설계 목적

* 상태별 책임 분리
* 유지보수성 향상
* 확장성 확보

### 주요 상태

* Wander
* Chase
* Attack
* Hit
* Dead

---

## StatusEffect 기반 상태이상 시스템


<img width="1123" height="397" alt="image" src="https://github.com/user-attachments/assets/2bcf4c89-b57d-41d2-b241-88c876785dde" />


### 설계 목적

* 상태이상 로직 통합
* 중복 코드 제거
* 신규 효과 확장 용이

### 라이프사이클

Apply → Update → Remove

---

## MVP 패턴 기반 인벤토리

<img width="1168" height="397" alt="image" src="https://github.com/user-attachments/assets/bc462bce-c49e-48ee-ba23-0747d825dff6" />

### 구조

View → Presenter → Model

### 적용 효과

* UI와 비즈니스 로직 분리
* 유지보수성 향상
* 재사용성 향상

---

# Project Retrospective

### 개발 과정에서 얻은 경험

* FSM을 활용한 상태 기반 AI 설계 경험
* StatusEffect를 활용한 확장 가능한 구조 설계 경험
* MVP 패턴을 활용한 UI 아키텍처 설계 경험

### 향후 개선 계획

* 몬스터 AI 고도화
* 상태이상 추가
* 최적화 적용
* Addressables 활용

---

# Links

### Gameplay Video

(YouTube 링크)

### GitHub

[(Repository 링크)](https://github.com/pku1302/Adventure-Game)

### Play Build

[(유니티 플레이 링크)](https://play.unity.com/ko/games/352d00d0-713b-44f3-8917-8b4bd2219511/2d-adventure-game)
