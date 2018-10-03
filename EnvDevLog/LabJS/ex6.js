class Car {

    constructor (engine, color, speed, wheels = 4) {
      this.engine = engine;
      this.color = color;
      this.speed = speed;
      this.wheels = wheels;
      this.team = "";
      this.gas = 0;
    }
  
    print() {
      console.log(`${this.team} F1 Car => ${this.engine} ${this.speed}\n`);
    }
  
    fillGas() {
      this.gas = 14500;
    }
  }
  
  class Ferrari extends Car {
  
    constructor (pilot, aerodynamic) {
      super("v8", "red", 250);
      this.team = "Ferrari";
      this.pilot = pilot;
      this.aerodynamic = aerodynamic;
    }
  }
  
  class Mclaren extends Car {
  
    constructor (pilot, turbo) {
      super("v6", "orange", 240);
      this.team = "Mclaren";
      this.pilot = pilot;
      this.turbo = turbo;
    }
  }
  
  class Redbull extends Car {
  
    constructor (pilot, wheels) {
      super("v12", "blue", 260);
      this.team = "Redbull";
      this.pilot = pilot;
      this.wheels = wheels;
    }
  }
  
  function checkEngine (car) {
    return typeof car.engine !== "undefined";
  }
  
  function checkWheels (car) {
    return car.wheels === 4;
  }
  
  // Get teams from a group of cars
  function getTeams (cars) {
    const teams = [];
    for (let car of cars) {
      if (teams.includes(car.team)) {
        teams.push(car.team);
      }
    }
  
    return teams;
  }
  
  function addTeam(carreerCars, teamCars) {
    for (let car of teamCars) {
        if (checkEngine(car) && checkWheels(car)) {
            car.fillGas();
            carreerCars.push(car);
        }
    }
  }
  
  function race() {
    const carreer = {
        cars: [],
        teams: [],
        laps: 20,
        finish: false
    };
  
    const cars = [];
  
    // Initialize cars
    const car1 = new Ferrari("Vettel", "speed");
    const car2 = new Mclaren("Alonso", true);
    const car3 = new Redbull("Ricciardo", 3);
    const car4 = new Redbull("Verstappen", 4);
  
    const teamFerrari = [car1];
    const teamMclaren = [car2];
    const teamRedbull = [car3, car4];
  
  
    addTeam(carreer.cars, teamFerrari);
    addTeam(carreer.cars, teamMclaren);
    addTeam(carreer.cars, teamRedbull);
  
    carreer.teams = getTeams(carreer.cars);
  
    //Set Initial lap
    let currentLap = 0;
  
    //Start the career
    while (currentLap <= carreer.laps) {
        for (let car of carreer.cars) {
            car.gas = car.gas - car.speed**2 / 100;
        }
  
        currentLap++;
    }
  
    carreer.finish = true;
  }
  
  race();