
favoriteCity = localStorage.getItem('favoriteCity').split(',')

function favoriteCityEdit(cityName) {
    favoriteCity = localStorage.getItem('favoriteCity').split(',')
    if (favoriteCity.includes(cityName)) { // varsa
        $("#FavoriteBtn h4")[0].innerText = "Favori Kaldır"
        $("#FavoriteBtn i")[0].classList.value = "fa-solid fa-star fa-2xl";
    }
    else { // yoksa

        $("#FavoriteBtn h4")[0].innerText = "Favori Ekle"
        $("#FavoriteBtn i")[0].classList.value = "fa-regular fa-star fa-2xl";
    }
    showFavoriteList();

}


$("#FavoriteBtn").click(function () {
    if (favoriteCity.includes(this.dataset.city)) { // varsa
        const index = favoriteCity.indexOf(this.dataset.city);
        if (index > -1) {
            favoriteCity.splice(index, 1);
        }
    }
    else { // yoksa
        favoriteCity.push(this.dataset.city)
    }

    localStorage.setItem('favoriteCity', favoriteCity);
    favoriteCityEdit(this.dataset.city);
});

function showFavoriteList() {
    $("#FavoriteTableBody").children().empty();
    favoriteCity.slice(1).forEach(element => {
        $("#FavoriteTableBody").append(`
    <tr style = "width: 100%;display: flex;justify-content: space-between; align-items: flex-start;flex-direction: row;" class=""> 
        
<td>
        <form asp-controller="Weather" asp-action="Index" method="post" style = "margin-bottom:0">
			<input type="hidden" placeholder="Find your location..." asp-for="CityName" name="CityName" value="${element}"/>
			<input type="submit" value="${element}"> 
		</form>
</td>
        <td><button class="btn btn-danger btn-sm del-favorite" onClick="delFavorite(this,'${element}')" data-city="${element}">Sil</button></td>
    </tr>
    `)
    }
    );
}

showFavoriteList();

function delFavorite(e, cityName) {
    const index = favoriteCity.indexOf(cityName);
    if (index > -1) {
        favoriteCity.splice(index, 1);
    }
    localStorage.setItem('favoriteCity', favoriteCity);
    $(e).parent().parent().remove();
}