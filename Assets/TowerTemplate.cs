using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]//��� �޴� ����
public class TowerTemplate : ScriptableObject//������ ����
{
    public GameObject towerPrefab;//Ÿ�� ������ ����
    public GameObject followTowerPrefab;//�ӽ� Ÿ�� ������ ����
    public Weapon[] weapon;//Ÿ�� ����(����)

    [System.Serializable]//����ȭ�ؼ� �ν����Ϳ��� ������ �� �ְ�
    public struct Weapon//Ŭ���� �ܺο��� ���ο� ���� ������ �Ұ����ϵ��� Ŭ���� ���ο� ����ü ����
    {
        public Sprite sprite;//Ÿ�� ��������Ʈ
        public float damage;//������
        public float slow;//���� �ۼ�Ʈ 0.2=20%
        public float buff;//���ݷ� ������ 0.2 =20%
        public float rate;//���� �ӵ�
        public float range;//��Ÿ�
        public int cost;//��� -> 0: �Ǽ� 1~: ���׷��̵�
        public int sell;//�Ǹ� ���
    }
}
