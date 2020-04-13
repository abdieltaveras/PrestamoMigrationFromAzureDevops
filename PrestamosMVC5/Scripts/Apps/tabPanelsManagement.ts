/// debe ponerle el id btnNextTabPane al elemento que hay que pulsar para avanzar al proximo tab

$(document).ready(function () {
    // Get Dom Elements
    let navItems = $('.nav-item');
    navItems.on('click', setCurrentTab);
    let tabCount = navItems.length;
    let currentTab = 0;
    // Set Mask
    $("#btnNextTabPane").on("click", showTab);
    function showTab() {
        currentTab++;
        if (currentTab > tabCount - 1) { currentTab = 0; }
        let idNextTab = navItems[currentTab].id;
        let tabToSHow = $('#' + idNextTab);
        tabToSHow.tab('show');
    }
    function setCurrentTab() {
        for (var i = 0; i < tabCount; i++) {
            if (navItems[i].id == this.id) { currentTab = i; }
        }
    }
    function showAllTabs()
    {
        for (var i = -1; i < tabCount; i++) {
            currentTab = i;
            showTab();
        }
    }
});
