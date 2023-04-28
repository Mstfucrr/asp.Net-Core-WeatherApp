
favoriteCity = localStorage.getItem('favoriteCity').split(',')

function favoriteCityEdit(cityName) {
	if (favoriteCity.includes(cityName)) { // varsa
		$("a#FavoriteBtn h4")[0].innerText = "Favori Kaldır"
		$("a#FavoriteBtn i")[0].classList.value = "fa-solid fa-star fa-2xl";
	}
	else { // yoksa
		
		$("a#FavoriteBtn h4")[0].innerText = "Favori Ekle"
		$("a#FavoriteBtn i")[0].classList.value = "fa-regular fa-star fa-2xl";
	}
}


$("a#FavoriteBtn").click(function () {
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



