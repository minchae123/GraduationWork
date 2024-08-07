namespace echo17.EndlessBook.Demo02
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using echo17.EndlessBook;
    using UnityEngine.EventSystems;

    /// <summary>
    /// Table of contents page.
    /// Handles clicks on the chapters to jump to pages in the book
    /// </summary>
    public class PageView_02 : PageView
    {
        private bool isHandlingTouch = false; // 터치를 처리 중인지 확인하는 플래그
        private float touchCooldown = 0.5f; // 쿨다운 시간 (초 단위)
        private float lastTouchTime = -Mathf.Infinity; // 마지막 터치를 처리한 시간

        /// <summary>
        /// The name of the collider and what page number
        /// it is associated with
        /// </summary>
        [Serializable]
        public struct ChapterJump
        {
            public string gameObjectName;
            public int pageNumber;
        }

        public ChapterJump[] chapterJumps;

        protected override bool HandleHit(RaycastHit hit, BookActionDelegate action) // hit는 
        {
            // no action, just return
            if (action == null) return false;

            if (Time.time - lastTouchTime < touchCooldown)
            {
                return false;
            }

            // check each collider and jump to a page if that collider was hit.



            foreach (var chapterJump in chapterJumps)
            {
                if (chapterJump.gameObjectName == hit.collider.gameObject.name)
                {
                    print(hit.collider.gameObject.name);

                    isHandlingTouch = true;
                    lastTouchTime = Time.time;

                    //EventSystem.current.enabled = false;
                    GameManager.Instance.StartGame(chapterJump.pageNumber);
                    //action(BookActionTypeEnum.TurnPage, chapterJump.pageNumber); //누르면 실행됨

                    StartCoroutine(ResetTouchFlag());

                    return true;
                }
            }

            return false;
        }

        // 쿨다운 시간 후 터치 플래그를 리셋하는 코루틴
        private IEnumerator ResetTouchFlag()
        {
            yield return new WaitForSeconds(touchCooldown);
            isHandlingTouch = false;
        }
    }
}
