const olympicData = [
  { country: "Austria", code: "at", game: "Paralympics", sport: "Canoe Sprint", category: "Outdoor" },
  { country: "Brazil", code: "br", game: "Summer Olympics", sport: "Road Cycling", category: "Outdoor" },
  { country: "Canada", code: "ca", game: "Winter Olympics", sport: "Curling", category: "Indoor" },
  { country: "China", code: "cn", game: "Summer Olympics", sport: "Diving", category: "Indoor" },
  { country: "Cyprus", code: "cy", game: "Youth Olympic Games", sport: "Breakdancing", category: "Indoor" },
  { country: "Finland", code: "fi", game: "Youth Olympic Games", sport: "Skateboarding", category: "Outdoor" },
  { country: "France", code: "fr", game: "Youth Olympic Games", sport: "Breakdancing", category: "Indoor" },
  { country: "Germany", code: "de", game: "Summer Olympics", sport: "Diving", category: "Indoor" },
  { country: "Great Britain", code: "gb", game: "Winter Olympics", sport: "Curling", category: "Indoor" },
  { country: "Italy", code: "it", game: "Winter Olympics", sport: "Bobsleigh", category: "Outdoor" },
  { country: "Jamaica", code: "jm", game: "Winter Olympics", sport: "Bobsleigh", category: "Outdoor" },
  { country: "Japan", code: "jp", game: "Winter Olympics", sport: "Bobsleigh", category: "Outdoor" },
  { country: "Mexico", code: "mx", game: "Summer Olympics", sport: "Diving", category: "Indoor" },
  { country: "Netherlands", code: "nl", game: "Summer Olympics", sport: "Cycling", category: "Outdoor" },
  { country: "Pakistan", code: "pk", game: "Paralympics", sport: "Canoe Sprint", category: "Outdoor" },
  { country: "Portugal", code: "pt", game: "Youth Olympic Games", sport: "Skateboarding", category: "Outdoor" },
  { country: "Russia", code: "ru", game: "Youth Olympic Games", sport: "Breakdancing", category: "Indoor" },
  { country: "Slovakia", code: "sk", game: "Youth Olympic Games", sport: "Skateboarding", category: "Outdoor" },
  { country: "Sweden", code: "se", game: "Winter Olympics", sport: "Curling", category: "Indoor" },
  { country: "Thailand", code: "th", game: "Paralympics", sport: "Archery", category: "Indoor" },
  { country: "Ukraine", code: "ua", game: "Paralympics", sport: "Archery", category: "Indoor" },
  { country: "Uruguay", code: "uy", game: "Paralympics", sport: "Archery", category: "Indoor" },
  { country: "USA", code: "us", game: "Summer Olympics", sport: "Road Cycling", category: "Outdoor" },
  { country: "Zimbabwe", code: "zw", game: "Paralympics", sport: "Canoe Sprint", category: "Outdoor" }
];

// Sort alphabetically by Country Name
olympicData.sort((a, b) => a.country.localeCompare(b.country));

const grid = document.getElementById("flagGrid");
const tooltip = document.getElementById("tooltip");

function renderCards(data) {
  grid.innerHTML = "";

  if (data.length === 0) {
    grid.innerHTML = `<p style="grid-column: 1/-1; text-align: center; color: #777;">No matching records found.</p>`;
    return;
  }

  data.forEach(item => {
    const card = document.createElement("div");
    card.className = "card";
    
    card.innerHTML = `
      <img src="https://flagcdn.com/w160/${item.code}.png" alt="${item.country} Flag">
      <h3>${item.country}</h3>
    `;

    card.addEventListener("mousemove", (e) => {
      tooltip.style.display = "block";
      tooltip.style.left = e.pageX + 15 + "px";
      tooltip.style.top = e.pageY + 15 + "px";
      tooltip.innerHTML = `
        <strong>${item.country}</strong><br>
        Game: ${item.game}<br>
        Sport: ${item.sport}<br>
        Category: ${item.category}
      `;
    });

    card.addEventListener("mouseleave", () => {
      tooltip.style.display = "none";
    });

    grid.appendChild(card);
  });
}

function filterData() {
  const searchVal = document.getElementById("searchInput").value.toLowerCase().trim();
  const gameVal = document.getElementById("gameFilter").value;
  const categoryVal = document.getElementById("categoryFilter").value;

  const filtered = olympicData.filter(item => {
    const matchesSearch = item.country.toLowerCase().includes(searchVal) || item.sport.toLowerCase().includes(searchVal);
    const matchesGame = gameVal === "ALL" || item.game.toLowerCase() === gameVal.toLowerCase();
    const matchesCategory = categoryVal === "ALL" || item.category.toLowerCase() === categoryVal.toLowerCase();
    return matchesSearch && matchesGame && matchesCategory;
  });

  renderCards(filtered);
}

// Initial Render
renderCards(olympicData);