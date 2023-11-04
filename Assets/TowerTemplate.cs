using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]//어셋 메뉴 생성
public class TowerTemplate : ScriptableObject//데이터 관리
{
    public GameObject towerPrefab;//타워 프리팹 지정
    public GameObject followTowerPrefab;//임시 타워 프리팹 지정
    public Weapon[] weapon;//타워 종류(레벨)

    [System.Serializable]//직렬화해서 인스펙터에서 수정할 수 있게
    public struct Weapon//클래스 외부에서 새로운 변수 생성이 불가능하도록 클래스 내부에 구조체 생성
    {
        public Sprite sprite;//타워 스프라이트
        public float damage;//데미지
        public float slow;//감속 퍼센트 0.2=20%
        public float buff;//공격력 증가율 0.2 =20%
        public float rate;//공격 속도
        public float range;//사거리
        public int cost;//비용 -> 0: 건설 1~: 업그레이드
        public int sell;//판매 골드
    }
}
