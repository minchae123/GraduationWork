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

            // check each collider and jump to a page if that collider was hit.

            print(hit.collider.gameObject.name);

            foreach (var chapterJump in chapterJumps)
            {
                if (chapterJump.gameObjectName == hit.collider.gameObject.name)
                {
                    //EventSystem.current.enabled = false;
                    GameManager.Instance.StartGame(chapterJump.pageNumber);
                    //action(BookActionTypeEnum.TurnPage, chapterJump.pageNumber); //누르면 실행됨
                    return true;
                }
            }

            return false;
        }
    }
}
