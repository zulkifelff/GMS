using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories;
namespace DAL
{
    public class UnitOfWork : IDisposable
    {
        private bool disposed = false;
        private GMSEntities _context;

        //private UserRepository _userRepository;
        //private ProductRepository _productRepository;
        //private CommonRepository _commonRepository;
        //private CategoryRepository _categoryRepository;
        //private NewsletterRepository _newsletterRepository;
        //private FaqRepository _faqRepository;
        //private AdRepository _adRepository;
        //private ForumRepository _forumRepository;
        //private BlogRepository _blogRepository;
        //private SubscriberRepository _subscriberRepository;
        private GeneralRepository _generalRepository;
        private MemberRepository _memberRepository;
        private DesignationRepository _designationRepository;
        private DepartmentRepository _departmentRepository;
        private PackageRepository _packageRepository;
        private EmployeeRepository _employeeRepository;

        public UnitOfWork(GMSEntities context)
        {
            _context = context;
        }
        //public CommonRepository CommonRepository
        //{
        //    get
        //    {
        //        if (_commonRepository == null)
        //            _commonRepository = new CommonRepository(_context);
        //        return _commonRepository;
        //    }
        //}
        public EmployeeRepository EmployeeRepository
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_context);
                return _employeeRepository;
            }
        }
        public DepartmentRepository DepartmentRepository
        {
            get
            {
                if (_departmentRepository == null)
                    _departmentRepository = new DepartmentRepository(_context);
                return _departmentRepository;
            }
        }
        public DesignationRepository DesignationRepository
        {
            get
            {
                if (_designationRepository == null)
                    _designationRepository = new DesignationRepository(_context);
                return _designationRepository;
            }
        }
        public PackageRepository PackageRepository
        {
            get
            {
                if (_packageRepository == null)
                    _packageRepository = new PackageRepository(_context);
                return _packageRepository;
            }
        }

        public MemberRepository MemberRepository
        {
            get
            {
                if (_memberRepository == null)
                    _memberRepository = new MemberRepository(_context);
                return _memberRepository;
            }
        }

        public GeneralRepository GeneralRepository
        {
            get
            {
                if (_generalRepository == null)
                    _generalRepository = new GeneralRepository(_context);
                return _generalRepository;
            }
        }

        //public FaqRepository FaqRepository
        //{
        //    get
        //    {
        //        if (_faqRepository == null)
        //            _faqRepository = new FaqRepository(_context);
        //        return _faqRepository;
        //    }
        //}

        //public BlogRepository BlogRepository
        //{
        //    get
        //    {
        //        if (_blogRepository == null)
        //            _blogRepository = new BlogRepository(_context);
        //        return _blogRepository;
        //    }
        //}

        //public SubscriberRepository SubscriberRepository
        //{
        //    get
        //    {
        //        if (_subscriberRepository == null)
        //            _subscriberRepository = new SubscriberRepository(_context);
        //        return _subscriberRepository;
        //    }
        //}

        //public OrderRepository OrderRepository
        //{
        //    get
        //    {
        //        if (_orderRepository == null)
        //            _orderRepository = new OrderRepository(_context);
        //        return _orderRepository;
        //    }
        //}
        //public OrderTimeLineRepository OrderTimeLineRepository
        //{
        //    get
        //    {
        //        if (_orderTimeLineRepository == null)
        //            _orderTimeLineRepository = new OrderTimeLineRepository(_context);
        //        return _orderTimeLineRepository;
        //    }
        //}
        //public CouponRepository CouponRepository
        //{
        //    get
        //    {
        //        if (_couponRepository == null)
        //            _couponRepository = new CouponRepository(_context);
        //        return _couponRepository;
        //    }
        //}
        //public QueryRepository QueryRepository
        //{
        //    get
        //    {
        //        if (_queryRepository == null)
        //            _queryRepository = new QueryRepository(_context);
        //        return _queryRepository;
        //    }
        //}
        //public NewsletterRepository NewsletterRepository
        //{
        //    get
        //    {
        //        if (_newsletterRepository == null)
        //            _newsletterRepository = new NewsletterRepository(_context);
        //        return _newsletterRepository;
        //    }
        //}
        //public FoodRequestRepository FoodRequestRepository
        //{
        //    get
        //    {
        //        if (_foodRequestRepository == null)
        //            _foodRequestRepository = new FoodRequestRepository(_context);
        //        return _foodRequestRepository;
        //    }
        //}
        #region IDisposable Support 



        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
