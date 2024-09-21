import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Swiper, SwiperSlide } from 'swiper/react';
import { Navigation } from 'swiper/modules';
import 'swiper/css';
import 'swiper/css/navigation';
import './App.css'; // Twój plik z dodatkowymi stylami

const App = () => {
    const [tiers, setTiers] = useState([]);

    useEffect(() => {
        const fetchTiers = async () => {
            const response = await axios.get('http://localhost:5133/api/tier');
            setTiers(response.data);
        };
        fetchTiers();
    }, []);

    const handleTierClick = async (tierId) => {
        try {
            const response = await axios.post('http://localhost:5133/api/asdf/create-checkout-session', { tierId });
            window.location.href = response.data.url;
        } catch (error) {
            console.error('Error redirecting to Stripe:', error);
        }
    };

    return (
        <div className="app-container">
          <div className="title-container">
            <h1 className='title-text'>Gentleman's Club Warszawa</h1>
          </div>
          <div className='hero-section'> 
            <div className="gc-main-icon">
              <img class='gc-logo-image' src="/GCIcon.png" alt="Opis zdjęcia" />
            </div>
          </div>
          <div className="tier-container">
            <h1 className='tier-container-title'>Wybierz poziom subskrypcji</h1>
            <div className="swiper-container">
              <Swiper
                  modules={[Navigation]}
                  navigation={{ nextEl: '.swiper-button-next', prevEl: '.swiper-button-prev' }}
                  slidesPerView={3}
                  spaceBetween={32}
                  loop={true}
              >
                  {tiers.map((tier) => (
                      <SwiperSlide key={tier.id}>
                          <div className="author-profile__box">
                              <div className="author-profile__box--content">
                                  <div className="author-profile__threshold">
                                      <div className="author-profile__threshold--top">
                                          <div className="author-profile__threshold--header">
                                              <span className='author-profile__threshold--title'>{tier.name}</span>
                                              <br></br>
                                              <br></br>
                                              <span className="author-profile__threshold--value">{tier.price} PLN </span>
                                              <span className="author-profile__threshold--type">miesięcznie</span>
                                          </div>
                                          <br></br>
                                          <div className="author-profile__threshold--description">
                                              {tier.description}
                                          </div>
                                          <br></br>
                                          <div className="author-profile__threshold--action-wrapper">
                                              <button 
                                                  className="btn btn-complement btn-block"
                                                  onClick={() => handleTierClick(tier.id)}
                                              >
                                                  Subscribe
                                              </button>
                                          </div>
                                      </div>
                                  </div>
                              </div>
                          </div>
                      </SwiperSlide>
                  ))}

                  {/* Przyciski nawigacyjne */}
                  <div className="swiper-button-next"></div>
                  <div className="swiper-button-prev"></div>
              </Swiper>
              </div>
            </div>
            <p className='hero-text'>
              Jesienią 2014 roku grupa miłośników gier bitewnych i fabularnych zdecydowała się stworzyć w Warszawie miejsce, gdzie miłośnicy tych gier mogliby oddawać się swojemu hobby o dowolnej porze dnia (lub nocy). Tak właśnie powstał Gentleman's Club - warszawski klub gier bitewnych i RPG.
              </p>
        </div>
    );
};

export default App;
