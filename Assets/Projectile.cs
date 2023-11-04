using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Movement2D movement2D;//이동
    private Transform target;//타겟

    public void Setup(Transform target)
    {
        movement2D=GetComponent<Movement2D>();//movement2D의 컴포넌트 가져온다.
        this.target = target;//Tower가 지정한 Target를 가져온다
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null)//target이 존재하면
        {
            Vector3 direction = (target.position - transform.position).normalized;//타겟과 발사체의 상대적 위치
            movement2D.MoveTo(direction);//발사체를 타겟으로 이동
        }
        else//타겟이 없으면
        {
            Destroy(gameObject);//발사체 삭제
       }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;//적이 아닌 물체에 맞으면 무시
        if (collision.transform != target) return;  //적인 물ㅊ[ㅇ[ 맞았지만 타겟이 아닌 경우 무시

        collision.GetComponent<EnemyScript>().OnDie();//투사체가 타겟을 맞췄을 경우 적 사망 호출
        Destroy(gameObject);//발사체 오브젝트 파괴

    }
}
