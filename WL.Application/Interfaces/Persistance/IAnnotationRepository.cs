using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WL.Application.Annotations.Commands;
using WL.Domain;

namespace WL.Application.Interfaces.Persistance {

  public interface IAnnotationRepository : IRepository<Annotation> {

    Annotation Create(CreateAnnotationCommand cmd);
  }
}