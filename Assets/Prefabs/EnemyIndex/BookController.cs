using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BookController : MonoBehaviour
{
    public GameObject book; // Assign the GameObject with the Animator in the Inspector
    public Button openCloseButton; // Assign the UI Button in the Inspector

    // Buttons for flipping pages
    public Button playerButton;
    public Button npcsButton;
    public Button itemsButton;
    public Button enemiesButton;
    public Button bossButton;

    // GameObjects to enable/disable
    public GameObject playerPage;
    public GameObject npcsPage;
    public GameObject itemsPage;
    public GameObject enemiesPage;
    public GameObject bossPage;

    // Bookmarks
    public GameObject playerBookmark;
    public GameObject npcsBookmark;
    public GameObject itemsBookmark;
    public GameObject enemiesBookmark;
    public GameObject bossBookmark;

    private Animator animator;

    // Animator state names
    private const string OpenBookState = "OpenBook";
    private const string CloseBookState = "CloseBook";
    private const string BookClosedIdleState = "BookClosedIdle";
    private const string BookOpenIdleState = "BookOpenIdle";
    private const string PageFlipState = "PageFlip";

    private bool firstPageFlip = true;

    void Start()
    {
        if (book != null)
        {
            animator = book.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Book GameObject is not assigned.");
        }

        if (openCloseButton != null)
        {
            openCloseButton.onClick.AddListener(HandleOpenCloseButtonClick);
        }
        else
        {
            Debug.LogError("Open/Close Button is not assigned.");
        }

        // Add listeners for the new buttons
        if (playerButton != null)
            playerButton.onClick.AddListener(() => HandleFlipPageButtonClick(playerPage, playerBookmark));

        if (npcsButton != null)
            npcsButton.onClick.AddListener(() => HandleFlipPageButtonClick(npcsPage, npcsBookmark));

        if (itemsButton != null)
            itemsButton.onClick.AddListener(() => HandleFlipPageButtonClick(itemsPage, itemsBookmark));

        if (enemiesButton != null)
            enemiesButton.onClick.AddListener(() => HandleFlipPageButtonClick(enemiesPage, enemiesBookmark));

        if (bossButton != null)
            bossButton.onClick.AddListener(() => HandleFlipPageButtonClick(bossPage, bossBookmark));
    }

    void Update()
    {
        if (animator == null)
        {
            return;
        }

        // Handle state transitions for first page flip
        if (firstPageFlip && animator.GetCurrentAnimatorStateInfo(0).IsName(BookOpenIdleState) &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            animator.SetBool("flipPage", true);
            animator.SetBool("openIdle", false);
        }

        // Reset flipPage to false after PageFlip animation finishes
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(PageFlipState) &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            animator.SetBool("flipPage", false);
            animator.SetBool("openIdle", true);
            firstPageFlip = false; // No longer the first page flip
        }
    }

    void HandleOpenCloseButtonClick()
    {
        if (IsCurrentState(BookClosedIdleState) || IsCurrentState(CloseBookState))
        {
            animator.SetBool("openBook", true);
        }
        else if (IsCurrentState(OpenBookState) || IsCurrentState(BookOpenIdleState))
        {
            animator.SetBool("openBook", false);
        }
    }

    void HandleFlipPageButtonClick(GameObject page, GameObject bookmark)
    {
        // Check if the book is currently open before proceeding
        if (!IsCurrentState(OpenBookState) && !IsCurrentState(BookOpenIdleState))
        {
            return; // Exit the method if the book is closed or closing
        }

        if (firstPageFlip)
        {
            animator.SetBool("openIdle", true);
        }
        else
        {
            animator.SetBool("flipPage", true);
            animator.SetBool("openIdle", false);
        }

        // Handle enabling/disabling pages
        StartCoroutine(EnableDisablePage(page));

        // Handle bookmarks
        StartCoroutine(HandleBookmarks(bookmark));
    }


    IEnumerator EnableDisablePage(GameObject page)
    {
        // Disable all pages immediately
        playerPage.SetActive(false);
        npcsPage.SetActive(false);
        itemsPage.SetActive(false);
        enemiesPage.SetActive(false);
        bossPage.SetActive(false);

        // Determine the delay based on the current animation state
        float delay = IsCurrentState(OpenBookState) ? 2.0f : 1.0f;

        // Wait for the determined delay before enabling the clicked page
        yield return new WaitForSeconds(delay);

        page.SetActive(true);
    }

    IEnumerator HandleBookmarks(GameObject activeBookmark)
    {
        // Set markDown to false for all bookmarks except the active bookmark
        if (activeBookmark != playerBookmark)
            playerBookmark.GetComponent<Animator>().SetBool("markDown", false);

        if (activeBookmark != npcsBookmark)
            npcsBookmark.GetComponent<Animator>().SetBool("markDown", false);

        if (activeBookmark != itemsBookmark)
            itemsBookmark.GetComponent<Animator>().SetBool("markDown", false);

        if (activeBookmark != enemiesBookmark)
            enemiesBookmark.GetComponent<Animator>().SetBool("markDown", false);

        if (activeBookmark != bossBookmark)
            bossBookmark.GetComponent<Animator>().SetBool("markDown", false);

        // Wait for 1 second
        yield return new WaitForSeconds(1);

        // Set markDown to true for the active bookmark
        activeBookmark.GetComponent<Animator>().SetBool("markDown", true);
    }

    bool IsCurrentState(string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
}
