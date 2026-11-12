<%@ Page Title="" Language="C#" MasterPageFile="~/moprofmaster.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="MoProf_bse25aft1_250429135._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

            <!-- index start -->


        <!-- hero section start -->

        <section id="hero" style="background-image: url(images/billboard-bg.png); background-repeat: no-repeat;">
            <div class="container">
                <div class="row align-items-center">
                    <div class="col-md-6 pe-5 mt-5 mt-md-0">
                        <h2 class="display-1 text-uppercase">Master Your Courses Before Exams</h2>
                        <p class="fs-4 my-4 pb-2">Courses For All Levels Taught By Certified Tutors!</p>
                        <div>
                                <div class="d-flex align-items-center">
                                    <h5 class="px-4 py-3 w-100">Sign Up Now to Gain Access</h5>
                                 <asp:Button ID="btnTopregiser" runat="server" Text="Get Started Now" CssClass="btn btn-primary rounded-4 px-3 py-2" OnClick="btnTopregiser_Click" />

                                </div>
                        </div>
                    </div>
                    <div class="col-md-6 mt-5">
                        <img src="images/billboard-img.jpg" alt="img" class="img-fluid">
                    </div>
                </div>
            </div>
        </section>

        <br />

        <!-- hero section end -->

        <!-- bubbles start -->

        <section id="features">
            <div class="feature-box container">
                <div class="row ">
                    <div class="col-md-3 col-sm-6 mb-5 mb-md-0">
                        <div class="feature-item py-5  rounded-4">
                            <div class="feature-detail text-center">
                                <h2 class="feature-title">100+</h2>
                                <h6 class="feature-info text-uppercase">instructors</h6>

                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 mb-5 mb-md-0">
                        <div class="feature-item py-5  rounded-4">
                            <div class="feature-detail text-center">
                                <h2 class="feature-title">20+</h2>
                                <h6 class="feature-info text-uppercase">courses</h6>

                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 mb-5 mb-md-0">
                        <div class="feature-item py-5  rounded-4">
                            <div class="feature-detail text-center">
                                <h2 class="feature-title">18</h2>
                                <h6 class="feature-info text-uppercase">learning levels offered</h6>

                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 mb-5 mb-md-0">
                        <div class="feature-item py-5  rounded-4">
                            <div class="feature-detail text-center">
                                <h2 class="feature-title">2,000+</h2>
                                <h6 class="feature-info text-uppercase">happy members</h6>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>

        <!-- bubbles end -->

        <!-- about us start -->

        <section id="about" class="padding-medium mt-xl-5">
            <div class="container">
                <div class="row align-items-center mt-xl-5">
                    <div class="offset-md-1 col-md-5">
                        <img src="images/about-img.jpg" alt="img" class="img-fluid rounded-circle">
                    </div>
                    <div class="col-md-5 mt-5 mt-md-0">
                        <div class="mb-3">
                            <p class="text-secondary ">Learn more about us</p>
                            <h2 class="display-6 fw-semibold">About Us</h2>
                        </div>
                        <p>
                            MoProf is where knowledge meets opportunity. We connect learners and tutors across Mauritius, offering diverse courses for every age, skill, and passion, because education changes lives.
                        </p>
                        <div class="d-flex">
                            <svg xmlns="http://www.w3.org/2000/svg" width="22px" height="22px">
                                <use href="#tick-circle" class="text-secondary" />
                            </svg>
                            <p class="ps-4">Browse and filter tutors by subject, category, district, or name instantly</p>
                        </div>
                        <div class="d-flex">
                            <svg xmlns="http://www.w3.org/2000/svg" width="22px" height="22px">
                                <use href="#tick-circle" class="text-secondary" />
                            </svg>
                            <p class="ps-4">View tutor qualifications, experience, fees, schedule, and availability at a glance</p>
                        </div>
                        <div class="d-flex">
                            <svg xmlns="http://www.w3.org/2000/svg" width="22px" height="22px">
                                <use href="#tick-circle" class="text-secondary" />
                            </svg>
                            <p class="ps-4">Earn badges for milestones like first booking, reviews, and learning streaks</p>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <!-- about us end -->

        <!-- categories start -->

        <section id="category">
            <div class="container ">
                <div class="d-md-flex justify-content-between align-items-center">
                    <div>
                        <p class="text-secondary ">Pick your category and get started</p>
                        <h2 class="display-6 fw-semibold">Popular Categories</h2>
                    </div>
                    <div class="mt-4 mt-md-0">

                        <asp:Button ID="btnViewCategories" runat="server" Text="View All Categories"
                            CssClass="btn btn-primary px-5 py-3" OnClick="btnViewCategories_Click" />

                    </div>
                </div>
                <div class="row g-md-3 mt-2">
                    <div class="col-md-4">
                        <div class="primary rounded-3 p-4 my-3">
                            <div class="d-flex align-items-center">
                                <i class="fas fa-palette svg-primary fa-fw" style="font-size: 1.25rem;"></i>
                                <div class="ps-4">
                                    <p class="category-paragraph fw-bold text-uppercase mb-1">Design, Arts & Creativity</p>
                                    <p class="category-paragraph m-0">30 courses</p>
                                </div>
                            </div>
                        </div>
                        <div class="tertiary rounded-3 p-4 my-3">
                            <div class="d-flex align-items-center">
                                <i class="fas fa-language svg-tertiary fa-fw" style="font-size: 1.25rem;"></i>
                                <div class="ps-4">
                                    <p class="category-paragraph fw-bold text-uppercase mb-1">Languages & Communication</p>
                                    <p class="category-paragraph m-0">50 courses</p>
                                </div>
                            </div>
                        </div>
                        <div class="secondary rounded-3 p-4 my-3">
                            <div class="d-flex align-items-center">
                                <i class="fas fa-calculator svg-secondary fa-fw" style="font-size: 1.25rem;"></i>
                                <div class="ps-4">
                                    <p class="category-paragraph fw-bold text-uppercase mb-1">Mathematics</p>
                                    <p class="category-paragraph m-0">50 courses</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="gray rounded-3 p-4 my-3">
                            <div class="d-flex align-items-center">
                                <i class="fas fa-flask svg-gray fa-fw" style="font-size: 1.25rem;"></i>
                                <div class="ps-4">
                                    <p class="category-paragraph fw-bold text-uppercase mb-1">Sciences</p>
                                    <p class="category-paragraph m-0">22 courses</p>
                                </div>
                            </div>
                        </div>
                        <div class="secondary rounded-3 p-4 my-3">
                            <div class="d-flex align-items-center">
                                <i class="fas fa-microchip svg-secondary fa-fw" style="font-size: 1.25rem;"></i>
                                <div class="ps-4">
                                    <p class="category-paragraph fw-bold text-uppercase mb-1">Engineering & Technology</p>
                                    <p class="category-paragraph m-0">16 courses</p>
                                </div>
                            </div>
                        </div>
                        <div class="tertiary rounded-3 p-4 my-3">
                            <div class="d-flex align-items-center">
                                <i class="fas fa-heart svg-tertiary fa-fw" style="font-size: 1.25rem;"></i>
                                <div class="ps-4">
                                    <p class="category-paragraph fw-bold text-uppercase mb-1">Health & Fitness</p>
                                    <p class="category-paragraph m-0">24 courses</p>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="col-md-4">
                        <div class="tertiary rounded-3 p-4 my-3">
                            <div class="d-flex align-items-center">
                                <i class="fas fa-chart-line svg-tertiary fa-fw" style="font-size: 1.25rem;"></i>
                                <div class="ps-4">
                                    <p class="category-paragraph fw-bold text-uppercase mb-1">Business & Digital Skills</p>
                                    <p class="category-paragraph m-0">24 courses</p>
                                </div>
                            </div>
                        </div>
                        <div class="primary rounded-3 p-4 my-3">
                            <div class="d-flex align-items-center">
                                <i class="fas fa-utensils svg-primary fa-fw" style="font-size: 1.25rem;"></i>
                                <div class="ps-4">
                                    <p class="category-paragraph fw-bold text-uppercase mb-1">Lifestyle & Hospitality</p>
                                    <p class="category-paragraph m-0">39 courses</p>
                                </div>
                            </div>
                        </div>
                        <div class="gray rounded-3 p-4 my-3">
                            <div class="d-flex align-items-center">
                                <i class="fas fa-check-circle svg-gray fa-fw" style="font-size: 1.25rem;"></i>
                                <div class="ps-4">
                                    <p class="category-paragraph fw-bold text-uppercase mb-1">Personal Development & Test Prep</p>
                                    <p class="category-paragraph m-0">25 courses</p>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>

        <!-- categories end -->

        <!-- online courses start -->

        <section id="courses" class="padding-medium">
            <div class="container">
                <div class="text-center mb-5">
                    <p class="text-secondary">By Highly Rated Tutors</p>
                    <h2 class="display-6 fw-semibold">Explore Our Most Popular Courses</h2>
                </div>

                <div class="row">
                    <div class="col-sm-6 col-lg-4 col-xl-3 mb-5">
                        <div class="z-1 position-absolute m-4">
                            <span class="badge text-white bg-secondary">New</span>
                        </div>
                        <div class="card rounded-4 border-0 shadow-sm p-3 position-relative">
                            <a href="courses-details.html">
                                <img src="images/item1.jpg" class="img-fluid rounded-3" alt="image"></a>
                            <div class="card-body p-0">

                                <div class="d-flex justify-content-between my-3">
                                    <p class="text-black-50 fw-bold text-uppercase m-0">Engineering and Technology</p>
                                    <div class="d-flex align-items-center">
                                        <svg width="20" height="20">
                                            <use xlink:href="#clock" class="text-black-50"></use>
                                        </svg>
                                        <p class="text-black-50 fw-bold text-uppercase m-0">Uni</p>
                                    </div>
                                </div>

                                <a href="courses-details.html">
                                    <h5 class="course-title py-2 m-0">Software Security</h5>
                                </a>

                                <div class="card-text">
                                    <span class="rating d-flex align-items-center mt-3">
                                        <p class="text-muted fw-semibold m-0 me-2">By: James Willam</p>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                    </span>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 col-lg-4 col-xl-3 mb-5">
                        <div class="z-1 position-absolute m-4">
                            <span class="badge text-white bg-secondary">New</span>
                        </div>
                        <div class="card rounded-4 border-0 shadow-sm p-3 position-relative">
                            <a href="courses-details.html">
                                <img src="images/item2.jpg" class="img-fluid rounded-3" alt="image"></a>
                            <div class="card-body p-0">

                                <div class="d-flex justify-content-between my-3">
                                    <p class="text-black-50 fw-bold text-uppercase m-0">Business & Digital Skills</</p>
                                    <div class="d-flex align-items-center">
                                        <svg width="20" height="20">
                                            <use xlink:href="#clock" class="text-black-50"></use>
                                        </svg>
                                        <p class="text-black-50 fw-bold text-uppercase m-0">HSC</p>
                                    </div>
                                </div>

                                <a href="courses-details.html">
                                    <h5 class="course-title py-2 m-0">Economics</h5>
                                </a>

                                <div class="card-text">
                                    <span class="rating d-flex align-items-center mt-3">
                                        <p class="text-muted fw-semibold m-0 me-2">By: James Willam</p>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                    </span>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 col-lg-4 col-xl-3 mb-5">
                        <div class="card rounded-4 border-0 shadow-sm p-3 position-relative">
                            <a href="courses-details.html">
                                <img src="images/item3.jpg" class="img-fluid rounded-3" alt="image"></a>
                            <div class="card-body p-0">

                                <div class="d-flex justify-content-between my-3">
                                    <p class="text-black-50 fw-bold text-uppercase m-0">Design, Arts & Creativity</p>
                                    <div class="d-flex align-items-center">
                                        <svg width="20" height="20">
                                            <use xlink:href="#clock" class="text-black-50"></use>
                                        </svg>
                                        <p class="text-black-50 fw-bold text-uppercase m-0">Uni</p>
                                    </div>
                                </div>

                                <a href="courses-details.html">
                                    <h5 class="course-title py-2 m-0">Fashion</h5>
                                </a>

                                <div class="card-text">
                                    <span class="rating d-flex align-items-center mt-3">
                                        <p class="text-muted fw-semibold m-0 me-2">By: James Willam</p>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                    </span>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 col-lg-4 col-xl-3 mb-5">
                        <div class="card rounded-4 border-0 shadow-sm p-3 position-relative">
                            <a href="courses-details.html">
                                <img src="images/item4.jpg" class="img-fluid rounded-3" alt="image"></a>
                            <div class="card-body p-0">

                                <div class="d-flex justify-content-between my-3">
                                    <p class="text-black-50 fw-bold text-uppercase m-0">Business & Digital Skills</p>
                                    <div class="d-flex align-items-center">
                                        <svg width="20" height="20">
                                            <use xlink:href="#clock" class="text-black-50"></use>
                                        </svg>
                                        <p class="text-black-50 fw-bold text-uppercase m-0">IBDP</p>
                                    </div>
                                </div>

                                <a href="courses-details.html">
                                    <h5 class="course-title py-2 m-0">Business Managment</h5>
                                </a>

                                <div class="card-text">
                                    <span class="rating d-flex align-items-center mt-3">
                                        <p class="text-muted fw-semibold m-0 me-2">By: James Willam</p>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                    </span>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 col-lg-4 col-xl-3 mb-5">
                        <div class="card rounded-4 border-0 shadow-sm p-3 position-relative">
                            <a href="courses-details.html">
                                <img src="images/item5.jpg" class="img-fluid rounded-3" alt="image"></a>
                            <div class="card-body p-0">

                                <div class="d-flex justify-content-between my-3">
                                    <p class="text-black-50 fw-bold text-uppercase m-0">Sciences</p>
                                    <div class="d-flex align-items-center">
                                        <svg width="20" height="20">
                                            <use xlink:href="#clock" class="text-black-50"></use>
                                        </svg>
                                        <p class="text-black-50 fw-bold text-uppercase m-0">Uni</p>
                                    </div>
                                </div>

                                <a href="courses-details.html">
                                    <h5 class="course-title py-2 m-0">Biology</h5>
                                </a>

                                <div class="card-text">
                                    <span class="rating d-flex align-items-center mt-3">
                                        <p class="text-muted fw-semibold m-0 me-2">By: James Willam</p>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                    </span>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 col-lg-4 col-xl-3 mb-5">
                        <div class="z-1 position-absolute m-4">
                            <span class="badge text-white bg-secondary">New</span>
                        </div>
                        <div class="card rounded-4 border-0 shadow-sm p-3 position-relative">
                            <a href="courses-details.html">
                                <img src="images/item6.jpg" class="img-fluid rounded-3" alt="image"></a>
                            <div class="card-body p-0">

                                <div class="d-flex justify-content-between my-3">
                                    <p class="text-black-50 fw-bold text-uppercase m-0">Engineering & Technology</p>
                                    <div class="d-flex align-items-center">
                                        <svg width="20" height="20">
                                            <use xlink:href="#clock" class="text-black-50"></use>
                                        </svg>
                                        <p class="text-black-50 fw-bold text-uppercase m-0">SC</p>
                                    </div>
                                </div>

                                <a href="courses-details.html">
                                    <h5 class="course-title py-2 m-0">Computer Science</h5>
                                </a>

                                <div class="card-text">
                                    <span class="rating d-flex align-items-center mt-3">
                                        <p class="text-muted fw-semibold m-0 me-2">By: James Willam</p>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                    </span>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 col-lg-4 col-xl-3 mb-5">
                        <div class="card rounded-4 border-0 shadow-sm p-3 position-relative">
                            <a href="courses-details.html">
                                <img src="images/item7.jpg" class="img-fluid rounded-3" alt="image"></a>
                            <div class="card-body p-0">

                                <div class="d-flex justify-content-between my-3">
                                    <p class="text-black-50 fw-bold text-uppercase m-0">Design, Arts & Creativity</p>
                                    <div class="d-flex align-items-center">
                                        <svg width="20" height="20">
                                            <use xlink:href="#clock" class="text-black-50"></use>
                                        </svg>
                                        <p class="text-black-50 fw-bold text-uppercase m-0">HSC</p>
                                    </div>
                                </div>

                                <a href="courses-details.html">
                                    <h5 class="course-title py-2 m-0">Design & Technology</h5>
                                </a>

                                <div class="card-text">
                                    <span class="rating d-flex align-items-center mt-3">
                                        <p class="text-muted fw-semibold m-0 me-2">By: James Willam</p>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                    </span>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 col-lg-4 col-xl-3 mb-5">
                        <div class="card rounded-4 border-0 shadow-sm p-3 position-relative">
                            <a href="courses-details.html">
                                <img src="images/item8.jpg" class="img-fluid rounded-3" alt="image"></a>
                            <div class="card-body p-0">

                                <div class="d-flex justify-content-between my-3">
                                    <p class="text-black-50 fw-bold text-uppercase m-0">Sciences</p>
                                    <div class="d-flex align-items-center">
                                        <svg width="20" height="20">
                                            <use xlink:href="#clock" class="text-black-50"></use>
                                        </svg>
                                        <p class="text-black-50 fw-bold text-uppercase m-0">IGCSE</p>
                                    </div>
                                </div>

                                <a href="courses-details.html">
                                    <h5 class="course-title py-2 m-0">Physics</h5>
                                </a>

                                <div class="card-text">
                                    <span class="rating d-flex align-items-center mt-3">
                                        <p class="text-muted fw-semibold m-0 me-2">By: James Willam</p>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                        <iconify-icon icon="clarity:star-solid" class="text-primary"></iconify-icon>
                                    </span>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="mt-4 mt-md-0 text-center">
                    <asp:Button ID="btnViewCourses" runat="server" Text="View All Courses"
                        CssClass="btn btn-primary px-5 py-3" OnClick="btnViewCourses_Click" />
                </div>
            </div>
        </section>

        <!-- online courses end -->

        <!-- reviews start-->

        <section id="testimonial" class="padding-medium bg-primary-subtle">
            <div class="container">
                <div class="text-center mb-4">
                    <p class="text-secondary ">What our students say about us</p>
                    <h2 class="display-6 fw-semibold">Reviews</h2>
                </div>
                <div class="row">
                    <div class="offset-md-1 col-md-10">
                        <div class="swiper testimonial-swiper">
                            <div class="swiper-wrapper">

                                <div class="swiper-slide pe-md-5">
                                    <div class="my-4">
                                        <p class="text-muted">
                                            “Physics was my nightmare subject until I connected with a tutor here. 
                    They broke down complex concepts into simple, 
                    visual explanations that finally clicked. 
                    I walked into my 5th Form exams calm and prepared—and aced every paper! ”
                                        </p>
                                        <div class="row">
                                            <div class="col-3">
                                                <img src="images/reviwer1.jpg" alt="img" class="img-fluid rounded-circle">
                                            </div>
                                            <div class="col-9">
                                                <h5 class="m-0 mt-2">Recco Gracia</h5>
                                                <p class="text-muted">SC</p>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="swiper-slide pe-md-5">
                                    <div class="my-4">
                                        <p class="text-muted">
                                            “IB Chemistry was overwhelming until I found my tutor here. 
                    Their step-by-step approach to organic chemistry and practical lab guidance saved my grades. 
                    I earned my IB Diploma with a Level 7 in 
                    Chemistry—an achievement I never thought possible! ”
                                        </p>
                                        <div class="row">
                                            <div class="col-3">
                                                <img src="images/reviwer2.jpg" alt="img" class="img-fluid rounded-circle">
                                            </div>
                                            <div class="col-9">
                                                <h5 class="m-0 mt-2">Bicky Yeal</h5>
                                                <p class="text-muted">IBDP</p>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="swiper-slide pe-md-5">
                                    <div class="my-4">
                                        <p class="text-muted">
                                            “I needed advanced maths support for my Software Engineering degree, and this platform delivered. 
                    My tutor bridged the gap between theory and application perfectly. 
                    I graduated with distinction and landed my dream tech job immediately after. ”
                                        </p>
                                        <div class="row">
                                            <div class="col-3">
                                                <img src="images/reviwer3.jpg" alt="img" class="img-fluid rounded-circle">
                                            </div>
                                            <div class="col-9">
                                                <h5 class="m-0 mt-2">Lilly Will</h5>
                                                <p class="text-muted">BSc Software Engineering</p>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="swiper-slide pe-md-5">
                                    <div class="my-4">
                                        <p class="text-muted">
                                            “My Business and Economics tutors completely transformed my understanding. 
                    They used real-world case studies that made theory come alive.
                    I finished HSC with top grades and a genuine passion for how the business world works. ”
                                        </p>
                                        <div class="row">
                                            <div class="col-3">
                                                <img src="images/reviwer1.jpg" alt="img" class="img-fluid rounded-circle">
                                            </div>
                                            <div class="col-9">
                                                <h5 class="m-0 mt-2">Recco Gracia</h5>
                                                <p class="text-muted">HSC</p>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="swiper-slide pe-md-5">
                                    <div class="my-4">
                                        <p class="text-muted">
                                            “My daughter struggled with English and maths throughout Grade 6. 
                    Within weeks of finding her tutors on this platform, her confidence soared. 
                    She passed her exams with flying colours and actually enjoys learning now. I can't recommend this enough! ”
                                        </p>
                                        <div class="row">
                                            <div class="col-3">
                                                <img src="images/reviwer2.jpg" alt="img" class="img-fluid rounded-circle">
                                            </div>
                                            <div class="col-9">
                                                <h5 class="m-0 mt-2">Recco Gracia</h5>
                                                <p class="text-muted">PSAC</p>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="swiper-pagination"></div>

                        </div>
                    </div>
                </div>
            </div>


        </section>

        <!-- reviews end -->

        <!-- our teachers start -->

        <section id="teacher" class="padding-medium">
            <div class="container">
                <div class="text-center mb-5">
                    <p class="text-secondary">Meet our highly rated teachers</p>
                    <h2 class="display-6 fw-semibold">Our Teachers</h2>
                </div>

                <div class="row">
                    <div class="col mb-5">
                        <div class="team-member position-relative card rounded-4 border-0 shadow-sm p-3">
                            <div class="image-holder zoom-effect rounded-3">
                                <img src="images/teacher1.jpg" class="img-fluid rounded-3" alt="image">
                            </div>
                            <div class="card-body p-0">
                                <div class="text-center mt-3">
                                    <p class="fw-bold m-0">Anila Lee</p>
                                    <p class="text-secondary m-0">Art Teacher | IBDP</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col mb-5">
                        <div class="team-member position-relative card rounded-4 border-0 shadow-sm p-3">
                            <div class="image-holder zoom-effect rounded-3">
                                <img src="images/teacher2.jpg" class="img-fluid rounded-3" alt="image">
                            </div>
                            <div class="card-body p-0">
                                <div class="text-center mt-3">
                                    <p class="fw-bold m-0">Bruce Smith</p>
                                    <p class="text-secondary m-0">Business Teacher | HSC</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col mb-5">
                        <div class="team-member position-relative card rounded-4 border-0 shadow-sm p-3">
                            <div class="image-holder zoom-effect rounded-3">
                                <img src="images/teacher3.jpg" class="img-fluid rounded-3" alt="image">
                            </div>
                            <div class="card-body p-0">

                                <div class="text-center mt-3">
                                    <p class="fw-bold m-0">Jack Nicholson</p>
                                    <p class="text-secondary m-0">Physics Teacher | SC</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col mb-5">
                        <div class="team-member position-relative card rounded-4 border-0 shadow-sm p-3">
                            <div class="image-holder zoom-effect rounded-3">
                                <img src="images/teacher4.jpg" class="img-fluid rounded-3" alt="image">
                            </div>
                            <div class="card-body p-0">

                                <div class="text-center mt-3">
                                    <p class="fw-bold m-0">Calvin Ian</p>
                                    <p class="text-secondary m-0">Business Management Teacher | HSC</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col mb-5">
                        <div class="team-member position-relative card rounded-4 border-0 shadow-sm p-3">
                            <div class="image-holder zoom-effect rounded-3">
                                <img src="images/teacher5.jpg" class="img-fluid rounded-3" alt="image">
                            </div>
                            <div class="card-body p-0">

                                <div class="text-center mt-3">
                                    <p class="fw-bold m-0">Milye Sam</p>
                                    <p class="text-secondary m-0">Biology Teacher | SC</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </section>

        <!-- our teachers end -->

        <!-- core features start -->

        <section id="points" class="padding-medium pt-0">
            <div class="container">
                <div class="text-center">
                    <p class="text-secondary ">What features we provide</p>
                    <h2 class="display-6 fw-semibold">Core Features</h2>
                </div>
                <div class="row align-items-center mt-5">
                    <div class="col-md-6 pe-md-5">

                        <div class="d-flex">
                            <svg xmlns="http://www.w3.org/2000/svg" width="22px" height="22px">
                                <i class="fas fa-check-circle svg-secondary fa-fw" style="font-size: 1.25rem;"></i>
                            </svg>
                            <p class="ps-4">Browse and filter tutors by subject, category, district, or name instantly</p>
                        </div>
                        <div class="d-flex">
                            <svg xmlns="http://www.w3.org/2000/svg" width="22px" height="22px">
                                <i class="fas fa-check-circle svg-secondary fa-fw" style="font-size: 1.25rem;"></i>
                            </svg>
                            <p class="ps-4">View tutor qualifications, experience, fees, schedule, and availability at a glance</p>
                        </div>
                        <div class="d-flex">
                            <svg xmlns="http://www.w3.org/2000/svg" width="22px" height="22px">
                                <i class="fas fa-check-circle svg-secondary fa-fw" style="font-size: 1.25rem;"></i>
                            </svg>
                            <p class="ps-4">Upload academic results as PDF or images for tutors to review before booking</p>
                        </div>
                        <div class="d-flex">
                            <svg xmlns="http://www.w3.org/2000/svg" width="22px" height="22px">
                                <i class="fas fa-check-circle svg-secondary fa-fw" style="font-size: 1.25rem;"></i>
                            </svg>
                            <p class="ps-4">Request a seat with your preferred tutor and receive instant confirmation</p>
                        </div>

                    </div>
                    <div class="col-md-6 pe-md-5">

                        <div class="d-flex">
                            <svg xmlns="http://www.w3.org/2000/svg" width="22px" height="22px">
                                <i class="fas fa-check-circle svg-secondary fa-fw" style="font-size: 1.25rem;"></i>
                            </svg>
                            <p class="ps-4">Rate and review tutors—one recommendation per tutor per student</p>
                        </div>
                        <div class="d-flex">
                            <svg xmlns="http://www.w3.org/2000/svg" width="22px" height="22px">
                                <i class="fas fa-check-circle svg-secondary fa-fw" style="font-size: 1.25rem;"></i>
                            </svg>
                            <p class="ps-4">Tutors post updates and announcements for their enrolled students</p>
                        </div>
                        <div class="d-flex">
                            <svg xmlns="http://www.w3.org/2000/svg" width="22px" height="22px">
                                <i class="fas fa-check-circle svg-secondary fa-fw" style="font-size: 1.25rem;"></i>
                            </svg>
                            <p class="ps-4">Complete payments seamlessly after your tutor accepts your booking request</p>
                        </div>
                        <div class="d-flex">
                            <svg xmlns="http://www.w3.org/2000/svg" width="22px" height="22px">
                                <i class="fas fa-check-circle svg-secondary fa-fw" style="font-size: 1.25rem;"></i>
                            </svg>
                            <p class="ps-4">Earn badges for milestones like first booking, reviews, and learning streaks</p>
                        </div>

                    </div>
                </div>
            </div>
        </section>

        <!-- core features end -->

        <!-- registration start -->

        <section id="register">
            <div class="container padding-medium">
                <div class="row align-items-center">
                    <div class="offset-md-1 col-md-5 ">
                        <div>
                            <h2 class="display-6 fw-semibold">Sign Up Now and Gain Access to Top of the Line Education</h2>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <form id="form">
                            <div class="d-grid">
                                <button class="btn btn-primary px-5 py-3">Get Started now</button>
                            </div>
                        </form>
                    </div>

                </div>
            </div>
        </section>

        <!-- registration end -->

        <!-- index end -->

</asp:Content>
